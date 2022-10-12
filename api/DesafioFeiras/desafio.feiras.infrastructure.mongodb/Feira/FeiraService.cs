using AutoMapper;

namespace desafio.feiras.infrastructure.mongodb.Feira
{
    public class FeiraService : IFeiraService
    {
        private const string FEIRA_SEQUENCE_KEY = "FeiraId";

        private readonly IFeiraRepository repository;
        private readonly IMapper mapper;

        public FeiraService(IFeiraRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Create(domain.Feira entity)
        {
            var document = mapper.Map<FeiraDocument>(entity);
            await this.repository.InsertOne(document, FEIRA_SEQUENCE_KEY);
            return document.Id;
        }

        public Task Delete(int id) => this.repository.Delete(id);
        
        public async Task<domain.Feira> Get(int id)
        {
            var document = await this.repository.Get(id);
            return mapper.Map<domain.Feira>(document);
        }

        public async Task<IEnumerable<domain.Feira>> GetAll()
        {
            var documents = await this.repository.GetAll();
            return documents.Select(doc => mapper.Map<domain.Feira>(doc));
        }

        public Task Update(domain.Feira entity)
        {
            var document = mapper.Map<FeiraDocument>(entity);
            return this.repository.ReplaceOne(document);
        }
    }
}
