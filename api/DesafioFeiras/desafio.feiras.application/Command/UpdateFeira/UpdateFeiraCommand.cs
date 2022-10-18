using desafio.feiras.infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace desafio.feiras.application.Command.UpdateFeira
{
    /// <summary>
    /// Comando CQRS para atualização de dados de feira
    /// </summary>
    public class UpdateFeiraCommand : IRequestHandler<UpdateFeiraRequest>
    {
        private readonly IFeiraService service;
        private readonly ILogger<UpdateFeiraCommand> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">Serviço de infraestrutura para atualização de dados de feira</param>
        /// <param name="logger"></param>
        public UpdateFeiraCommand(IFeiraService service, ILogger<UpdateFeiraCommand> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        /// <summary>
        /// Executa a atualização dos dados de feira
        /// </summary>
        /// <param name="request">Dados da feira a atualizar</param>
        /// <param name="cancellationToken"></param>
        public async Task<Unit> Handle(UpdateFeiraRequest request, CancellationToken cancellationToken)
        {
            using (logger.BeginScope($"Atualizando feira {request.Identificador}"))
            {
                await service.Update(request);

                logger.LogInformation($"Feira {request.Nome} atualizada por identificador {request.Identificador}.");

                return Unit.Value;
            }
        }
    }
}
