using desafio.feiras.infrastructure.mongodb.Repository;

namespace desafio.feiras.infrastructure.mongodb.Feira
{
    public class FeiraRepository : MongoDBRepository<FeiraDocument>, IFeiraRepository
    {
        public FeiraRepository(MongoDBSettings settings) : base(settings)
        {
        }
    }
}
