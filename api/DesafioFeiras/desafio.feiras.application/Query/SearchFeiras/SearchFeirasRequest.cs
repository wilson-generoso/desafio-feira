using FluentValidation;
using MediatR;

namespace desafio.feiras.application.Query.SearchFeiras
{
    /// <summary>
    /// Dados de solicitação para filtro de pesquisa de feiras
    /// </summary>
    public class SearchFeirasRequest : IRequest<SearchFeirasResponse>
    {
        /// <summary>
        /// Nome do Distrito Municipal
        /// </summary>
        public string DistritoMunicipal { get; set; }

        /// <summary>
        /// Região conforme divisão do Município em 5 áreas
        /// </summary>
        public string RegiaoMunicipio5Areas { get; set; }

        /// <summary>
        /// Nome da feira livre
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Bairro de localização da feira livre
        /// </summary>
        public string Bairro { get; set; }
    }

    /// <summary>
    /// Validador de dados de solicitação para pesquisa de feiras
    /// </summary>
    public class SearchFeirasRequestValidator : AbstractValidator<SearchFeirasRequest>
    {
        /// <summary>
        /// Configura regras de validação
        /// </summary>
        public SearchFeirasRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.DistritoMunicipal)
                    || !string.IsNullOrEmpty(x.Nome)
                    || !string.IsNullOrEmpty(x.Bairro)
                    || !string.IsNullOrEmpty(x.RegiaoMunicipio5Areas))
                .WithMessage("Informe ao menos um filtro para pesquisa.");

            RuleFor(a => a.DistritoMunicipal)
                .MaximumLength(18)
                .When(x => !string.IsNullOrEmpty(x.DistritoMunicipal))
                .WithMessage("O nome do distrito municipal deve possuir no máximo 18 caracteres");

            RuleFor(a => a.RegiaoMunicipio5Areas)
                .MaximumLength(6)
                .When(x => !string.IsNullOrEmpty(x.RegiaoMunicipio5Areas))
                .WithMessage("A região em 5 áreas deve possuir no máximo 6 caracteres");

            RuleFor(a => a.Nome)
                .MaximumLength(30)
                .When(x => !string.IsNullOrEmpty(x.Nome))
                .WithMessage("O nome da feira livre deve possuir no máximo 30 caracteres");

            RuleFor(a => a.Bairro)
                .MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Bairro))
                .WithMessage("O bairro deve possuir no máximo 20 caracteres");
        }
    }
}
