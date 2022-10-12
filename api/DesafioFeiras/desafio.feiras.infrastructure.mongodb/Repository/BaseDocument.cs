using MongoDB.Bson.Serialization.Attributes;

namespace desafio.feiras.infrastructure.mongodb.Repository
{
    public abstract class BaseDocument
    {
        [BsonId]
        
        public virtual int Id { get; set; }
    }
}
