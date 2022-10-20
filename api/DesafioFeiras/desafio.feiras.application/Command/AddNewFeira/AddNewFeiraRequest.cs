using desafio.feiras.infrastructure;
using FluentValidation;
using MediatR;

namespace desafio.feiras.application.Command.AddNewFeira
{
    /// <summary>
    /// Dados de solicitação para operação de novo registro de feira
    /// </summary>
    public class AddNewFeiraRequest : IRequest<AddNewFeiraResponse>
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

    /// <summary>
    /// Validador de dados da solicitação de registro da nova feira
    /// </summary>
    public class AddNewFeiraRequestValidator : AbstractValidator<AddNewFeiraRequest>
    {
        /// <summary>
        /// Configura as regras de validação
        /// </summary>
        public AddNewFeiraRequestValidator()
        {
            RuleFor(a => a.Longitude)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a longitude")
                .MaximumLength(10).WithMessage("A longitude deve possuir no máximo 10 caracteres");

            RuleFor(a => a.Latitude)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a latitude")
                .MaximumLength(10).WithMessage("A latitude deve possuir no máximo 10 caracteres");

            RuleFor(a => a.SetorCensitario)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o setor censitário")
                .MaximumLength(15).WithMessage("O setor censitário deve possuir no máximo 15 caracteres");

            RuleFor(a => a.AreaPonderacao)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a área de ponderação")
                .MaximumLength(13).WithMessage("A área de ponderação deve possuir no máximo 13 caracteres");

            RuleFor(a => a.CodigoDistritoIBGE)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o código do distrito conforme IBGE")
                .MaximumLength(9).WithMessage("O código do distrito conforme IBGE deve possuir no máximo 9 caracteres");

            RuleFor(a => a.DistritoMunicipal)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome do distrito municipal")
                .MaximumLength(18).WithMessage("O nome do distrito municipal deve possuir no máximo 18 caracteres");

            RuleFor(a => a.CodigoSubprefeitura)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o código de uma das 31 subprefeituras (2003 a 2012)")
                .MaximumLength(2).WithMessage("O código de subprefeituras deve possuir no máximo 2 caracteres");

            RuleFor(a => a.Subprefeitura)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome da Subprefeitura (31 de 2003 até 2012)")
                .MaximumLength(25).WithMessage("O nome da Subprefeitura deve possuir no máximo 25 caracteres");

            RuleFor(a => a.RegiaoMunicipio5Areas)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a região conforme divisão do Município em 5 áreas")
                .MaximumLength(6).WithMessage("A região em 5 áreas deve possuir no máximo 6 caracteres");

            RuleFor(a => a.RegiaoMunicipio8Areas)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe a região conforme divisão do Município em 8 áreas")
                .MaximumLength(7).WithMessage("A região em 8 áreas deve possuir no máximo 7 caracteres");

            RuleFor(a => a.Nome)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome da feira livre atribuída pela Supervisão de Abastecimento")
                .MaximumLength(30).WithMessage("O nome da feira livre deve possuir no máximo 30 caracteres");

            RuleFor(a => a.Registro)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o número do registro da feira livre na PMSP")
                .MaximumLength(6).WithMessage("O número do registro deve possuir no máximo 6 caracteres");

            RuleFor(a => a.Logradouro)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage("Informe o nome do logradouro onde se localiza a feira livre")
                .MaximumLength(34).WithMessage("O nome do logradouro deve possuir no máximo 34 caracteres");

            RuleFor(a => a.NumeroLogradouro)
                .MaximumLength(5)
                .When(x => !string.IsNullOrEmpty(x.NumeroLogradouro))
                .WithMessage("O número do logradouro deve possuir no máximo 5 caracteres");

            RuleFor(a => a.Bairro)
                .MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Bairro))
                .WithMessage("O bairro deve possuir no máximo 20 caracteres");

            RuleFor(a => a.PontoReferencia)
                .MaximumLength(24)
                .When(x => !string.IsNullOrEmpty(x.PontoReferencia))
                .WithMessage("O ponto de referência deve possuir no máximo 24 caracteres");
        }

    }
}
