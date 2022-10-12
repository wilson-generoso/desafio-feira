using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace desafio.feiras.infrastructure.mongodb.Repository
{
    public interface IMongoDBRepository<TDocument> where TDocument : BaseDocument, new()
    {
        IMongoQueryable<TDocument> AsQueryable();
        Task<TDocument> Get(int id);
        Task<bool> Exists(int id);
        Task<IEnumerable<TDocument>> GetAll();
        Task Delete(int id);
        Task<IEnumerable<TDocument>> FindBy(Expression<Func<TDocument, bool>> filterExpression);
        Task InsertOne(TDocument document, string sequenceKey);
        Task ReplaceOne(TDocument document);
    }
}
