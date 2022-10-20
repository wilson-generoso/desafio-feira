using AutoMapper;
using desafio.feiras.infrastructure.mongodb.Feira;
using desafio.feiras.infrastructure.mongodb.Repository;

namespace desafio.feiras.infrastructure.mongodb
{
    public class MongoDBMapping : Profile
    {
        public MongoDBMapping()
        {
            CreateMap<domain.Feira, FeiraDocument>()
                .ForMember(doc => doc.Id, entity => entity.MapFrom(x => x.Identificador))
                .ReverseMap();
        }
    }
}
