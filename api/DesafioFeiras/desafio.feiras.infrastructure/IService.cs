using desafio.feiras.domain;

namespace desafio.feiras.infrastructure
{
    public interface IService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Get(int id);
        Task<int> Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
    }
}
