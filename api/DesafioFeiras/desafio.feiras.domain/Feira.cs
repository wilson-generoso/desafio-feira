namespace desafio.feiras.domain
{
    /// <summary>
    /// Dados que representam uma feira
    /// </summary>
    public class Feira : BaseEntity
    {
        /// <summary>
        /// Longitude da localização do estabelecimento no território do Município, conforme MDC
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// Latitude da localização do estabelecimento no território do Município, conforme MDC
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// Setor censitário conforme IBGE
        /// </summary>
        public string SetorCensitario { get; set; }
        /// <summary>
        /// Área de ponderação (agrupamento de setores censitários) conforme IBGE 2010
        /// </summary>
        public string AreaPonderacao { get; set; }
        /// <summary>
        /// Código do Distrito Municipal conforme IBGE
        /// </summary>
        public string CodigoDistritoIBGE { get; set; }
        /// <summary>
        /// Nome do Distrito Municipal
        /// </summary>
        public string DistritoMunicipal { get; set; }
        /// <summary>
        /// Código de cada uma das 31 Subprefeituras (2003 a 2012)
        /// </summary>
        public string CodigoSubprefeitura { get; set; }
        /// <summary>
        /// Nome da Subprefeitura (31 de 2003 até 2012)
        /// </summary>
        public string Subprefeitura { get; set; }
        /// <summary>
        /// Região conforme divisão do Município em 5 áreas
        /// </summary>
        public string RegiaoMunicipio5Areas { get; set; }
        /// <summary>
        /// Região conforme divisão do Município em 8 áreas
        /// </summary>
        public string RegiaoMunicipio8Areas { get; set; }
        /// <summary>
        /// Nome da feira livre
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Número do registro da feira livre na PMSP
        /// </summary>
        public string Registro { get; set; }
        /// <summary>
        /// Nome do logradouro onde se localiza a feira livre
        /// </summary>
        public string Logradouro { get; set; }
        /// <summary>
        /// Um número do logradouro onde se localiza a feira livre
        /// </summary>
        public string NumeroLogradouro { get; set; }
        /// <summary>
        /// Bairro de localização da feira livre
        /// </summary>
        public string Bairro { get; set; }
        /// <summary>
        /// Ponto de referência da localização da feira livre
        /// </summary>
        public string PontoReferencia { get; set; }
    }
}
