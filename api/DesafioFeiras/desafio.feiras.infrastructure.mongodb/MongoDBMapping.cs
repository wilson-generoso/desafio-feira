using AutoMapper;
using desafio.feiras.infrastructure.mongodb.Feira;
using desafio.feiras.infrastructure.mongodb.Repository;

namespace desafio.feiras.infrastructure.mongodb
{
    public class MongoDBMapping : Profile
    {
        public MongoDBMapping()
        {
            CreateMap<domain.BaseEntity, BaseDocument>()
                .ForMember(doc => doc.Id, entity => entity.MapFrom(x => x.Identificador));

            CreateMap<BaseDocument, domain.BaseEntity>()
                .ForMember(entity => entity.Identificador, doc => doc.MapFrom(x => x.Id));

            CreateMap<domain.Feira, FeiraDocument>().ReverseMap();
        }
    }
}
