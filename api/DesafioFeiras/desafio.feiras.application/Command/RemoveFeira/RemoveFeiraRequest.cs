using desafio.feiras.infrastructure;
using FluentValidation;
using MediatR;

namespace desafio.feiras.application.Command.RemoveFeira
{
    /// <summary>
    /// Dados de solicitação para remoção de feira
    /// </summary>
    public class RemoveFeiraRequest : IRequest
    {
        /// <summary>
        /// Identificador da feira a ser removida
        /// </summary>
        public int Identificador { get; set; }
    }

    /// <summary>
    /// Validador de dados da solicitação de registro da nova feira
    /// </summary>
    public class RemoveFeiraRequestValidator : AbstractValidator<RemoveFeiraRequest>
    {
        private readonly IFeiraService service;

        /// <summary>
        /// Configura as regras de validação
        /// </summary>
        /// <param name="service">Serviço de infraestrutura para verificar a existência da feira a ser removida</param>
        public RemoveFeiraRequestValidator(IFeiraService service)
        {
            this.service = service;

            RuleFor(x => x.Identificador)
                .MustAsync(HasFeira)
                .WithMessage("O identificador de feira informado não existe.");
        }

        /// <summary>
        /// Verifica se a feira existe
        /// </summary>
        /// <param name="identificador">Identificador da feira</param>
        /// <param name="token"></param>
        /// <returns>Indicador de existência ou não da feira</returns>
        private async Task<bool> HasFeira(int identificador, CancellationToken token)
        {
            return await service.Exists(identificador);
        }
    }
}
