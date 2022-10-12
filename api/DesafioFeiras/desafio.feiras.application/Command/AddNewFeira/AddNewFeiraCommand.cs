using AutoMapper;
using desafio.feiras.infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace desafio.feiras.application.Command.AddNewFeira
{
    /// <summary>
    /// Comando CQRS para registro de nova feira
    /// </summary>
    public class AddNewFeiraCommand : IRequestHandler<AddNewFeiraRequest, AddNewFeiraResponse>
    {
        private readonly IFeiraService service;
        private readonly IMapper mapper;
        private readonly ILogger<AddNewFeiraCommand> logger;

        /// <summary>
        /// </summary>
        /// <param name="service">Serviço de infraestrutura para persistência de feira</param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public AddNewFeiraCommand(IFeiraService service, IMapper mapper, ILogger<AddNewFeiraCommand> logger)
        {
            this.service = service;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Executa o registro de nova feira no serviço de infraestrutura
        /// </summary>
        /// <param name="request">Dados da solicitação para registro de nova feira</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response com o identificador gerado durante o registro de nova feira</returns>
        public async Task<AddNewFeiraResponse> Handle(AddNewFeiraRequest request, CancellationToken cancellationToken)
        {
            using (logger.BeginScope("Adicionando nova feira"))
            {
                var feira = mapper.Map<domain.Feira>(request);
                
                feira.Identificador = await service.Create(feira);

                logger.LogInformation($"Feira {feira.Nome} criada com identificador {feira.Identificador}.");

                return new AddNewFeiraResponse { Identificador = feira.Identificador };
            }
        }
    }
}
