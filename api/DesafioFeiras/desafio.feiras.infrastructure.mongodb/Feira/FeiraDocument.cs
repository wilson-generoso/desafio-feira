using desafio.feiras.infrastructure.mongodb.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace desafio.feiras.infrastructure.mongodb.Feira
{
    [BsonCollection("Feiras")]
    public class FeiraDocument : BaseDocument
    {
        [BsonRequired]
        public string Longitude { get; set; }
        [BsonRequired]
        public string Latitude { get; set; }
        [BsonRequired]
        public string SetorCensitario { get; set; }
        [BsonRequired]
        public string AreaPonderacao { get; set; }
        [BsonRequired]
        public string CodigoDistritoIBGE { get; set; }
        [BsonRequired]
        public string DistritoMunicipal { get; set; }
        [BsonRequired]
        public string CodigoSubprefeitura { get; set; }
        [BsonRequired]
        public string Subprefeitura { get; set; }
        [BsonRequired]
        public string RegiaoMunicipio5Areas { get; set; }
        [BsonRequired]
        public string RegiaoMunicipio8Areas { get; set; }
        [BsonRequired]
        public string Nome { get; set; }
        [BsonRequired]
        public string Registro { get; set; }
        [BsonRequired]
        public string Logradouro { get; set; }
        [BsonRequired]
        public string NumeroLogradouro { get; set; }
        [BsonRequired]
        public string Bairro { get; set; }
        [BsonRequired]
        public string PontoReferencia { get; set; }
    }
}
