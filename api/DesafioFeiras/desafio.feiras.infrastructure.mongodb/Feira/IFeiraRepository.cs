using desafio.feiras.infrastructure.mongodb.Feira;
using desafio.feiras.infrastructure.mongodb.Repository;

namespace desafio.feiras.infrastructure.mongodb.Feira
{
    public interface IFeiraRepository : IMongoDBRepository<FeiraDocument>
    {
    }
}
