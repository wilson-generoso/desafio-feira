using AutoMapper;
using desafio.feiras.infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace desafio.feiras.application.Query.SearchFeiras
{
    /// <summary>
    /// Query CQRS para pesquisa de feiras
    /// </summary>
    public class SearchFeirasQuery : IRequestHandler<SearchFeirasRequest, SearchFeirasResponse>
    {
        private readonly IFeiraService service;
        private readonly ILogger<SearchFeirasQuery> logger;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">Serviço de infraestrutura para pesquisa de feiras</param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public SearchFeirasQuery(IFeiraService service, ILogger<SearchFeirasQuery> logger, IMapper mapper)
        {
            this.service = service;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Executa pesquisa por feiras
        /// </summary>
        /// <param name="request">Filtros de pesquisa por feiras</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Resposta com a lista de feiras encontradas</returns>
        public async Task<SearchFeirasResponse> Handle(SearchFeirasRequest request, CancellationToken cancellationToken)
        {
            using(logger.BeginScope("Pesquisando por feiras"))
            {
                var feiras = await service.SearchForFeiras(request.DistritoMunicipal, request.RegiaoMunicipio5Areas, request.Nome, request.Bairro);

                var response = new SearchFeirasResponse();

                if (feiras.Any())
                    response.Feiras = feiras.Select(f => mapper.Map<SearchFeiraResponse>(f)).ToList();

                logger.LogInformation($"Foram encontradas {response.Feiras.Count()} feiras");

                return response;
            }
        }
    }
}
