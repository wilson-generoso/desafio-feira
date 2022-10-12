using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace desafio.feiras.infrastructure.mongodb.Repository
{
    public class MongoDBRepository<TDocument> : IMongoDBRepository<TDocument>
        where TDocument : BaseDocument, new()
    {
        protected readonly IMongoDatabase database;
        protected readonly IMongoCollection<TDocument> collection;

        public MongoDBRepository(MongoDBSettings settings)
        {
            var mongoSettings = new MongoClientSettings
            {
                ApplicationName = AppDomain.CurrentDomain.FriendlyName,
                Server = new MongoServerAddress(settings.Host, settings.Port),
            };

            database = new MongoClient(mongoSettings).GetDatabase(settings.DatabaseName);
            collection = this.GetCollection<TDocument>();
        }

        protected IMongoCollection<TDoc> GetCollection<TDoc>()
        {
            var collectionName = typeof(TDoc).GetCustomAttribute<BsonCollectionAttribute>(true)?.CollectionName ?? typeof(TDoc).Name;
            return database.GetCollection<TDoc>(collectionName);
        }

        public virtual IMongoQueryable<TDocument> AsQueryable() => collection.AsQueryable();

        public virtual async Task<IEnumerable<TDocument>> FindBy(Expression<Func<TDocument, bool>> filterExpression) => (await collection.FindAsync(filterExpression)).ToEnumerable();

        private FilterDefinition<TDocument> GetFilterById(int id) => Builders<TDocument>.Filter.Eq(doc => doc.Id, id);

        public async Task Delete(int id)
        {
            var filter = GetFilterById(id);
            await collection.DeleteOneAsync(filter);
        }

        public async Task InsertOne(TDocument document, string sequenceKey)
        {
            document.Id = await GetNextSequence(sequenceKey);
            await collection.InsertOneAsync(document);
        }

        public async Task ReplaceOne(TDocument document)
        {
            var filter = GetFilterById(document.Id);
            await collection.ReplaceOneAsync(filter, document);
        }

        public async Task<TDocument> Get(int id)
        {
            var filter = GetFilterById(id);
            return await collection.Find(filter).FirstAsync();
        }

        public async Task<IEnumerable<TDocument>> GetAll() => await collection.Find(Builders<TDocument>.Filter.Empty).ToListAsync();

        public async Task<bool> Exists(int id)
        {
            var filter = GetFilterById(id);
            return await collection.Find(filter).AnyAsync();
        }

        protected async Task<int> GetCurrentSequence(string key)
        {
            var counters = database.GetCollection<SequenceCounterDocument>("Counters");

            var counter = await counters.FindAsync(x => x.Key == key);

            if (counter.Any())
            {
                return (await counter.FirstAsync()).Sequence;
            }
            else
            {
                await counters.InsertOneAsync(new SequenceCounterDocument { Key = key, Sequence = 0 });
                return 0;
            }
        }

        protected async Task<int> GetNextSequence(string key)
        {
            var sequence = await GetCurrentSequence(key);

            sequence++;

            var counterFilter = Builders<SequenceCounterDocument>.Filter.Eq(s => s.Key, key);

            await database
                .GetCollection<SequenceCounterDocument>("Counters")
                .ReplaceOneAsync(counterFilter, new SequenceCounterDocument
                {
                    Key = key,
                    Sequence = sequence
                });

            return sequence;
        }
    }
}
