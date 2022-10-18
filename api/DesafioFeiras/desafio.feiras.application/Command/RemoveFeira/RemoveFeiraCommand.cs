using desafio.feiras.infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace desafio.feiras.application.Command.RemoveFeira
{
    /// <summary>
    /// Comando CQRS para remoção de feira
    /// </summary>
    public class RemoveFeiraCommand : IRequestHandler<RemoveFeiraRequest>
    {
        private readonly IFeiraService service;
        private readonly ILogger<RemoveFeiraCommand> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">Serviço de infraestrutura para remoção de feira</param>
        /// <param name="logger"></param>
        public RemoveFeiraCommand(IFeiraService service, ILogger<RemoveFeiraCommand> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Executa operação de remoção de feira
        /// </summary>
        /// <param name="request">Dados de solicitação para remoção de feira</param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(RemoveFeiraRequest request, CancellationToken cancellationToken)
        {
            using(logger.BeginScope($"Removendo conta {request.Identificador}"))
            {
                await service.Delete(request.Identificador);

                logger.LogInformation($"Feira {request.Identificador} removida com sucesso");

                return Unit.Value;
            }
        }
    }
}
