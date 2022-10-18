using desafio.feiras.api.Model;
using desafio.feiras.application.Command.AddNewFeira;
using desafio.feiras.application.Command.UpdateFeira;
using desafio.feiras.application.Query.SearchFeiras;
using Microsoft.AspNetCore.Mvc;

namespace desafio.feiras.api.Controllers.v1
{
    /// <summary>
    /// Controlador de API para manutenção de contas
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    public class FeiraController : BaseApiController
    {
        /// <summary>
        /// </summary>
        public FeiraController()
        {
        }
        
        /// <summary>
        /// Pesquisa por feiras cadastradas
        /// </summary>
        /// <param name="distrito">Nome do distrito municipal</param>
        /// <param name="regiao5">Região conforme divisão do Município em 5 áreas</param>
        /// <param name="nome_feira">Nome da feira livre</param>
        /// <param name="bairro">Bairro de localização da feira livre</param>
        /// <returns>Lista de contas cadastradas</returns>
        /// <response code="200">Executou a operação com sucesso</response>
        /// <response code="204">Executou a operação com sucesso, porém não encontrou resultados</response>
        /// <response code="500">Ocorreu um erro inesperado durante a execução da operação</response>
        /// <response code="400">Houveram falhas de negócio durante o processamento da operação</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SearchFeiraResponse))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [SwaggerNotRequiredParameters("distrito", "regiao5", "nome_feira", "bairro")]
        public async Task<IActionResult> SearchForFeiras(string distrito, string regiao5, string nome_feira, string bairro)
        {
            var response = await Mediator.Send(new SearchFeirasRequest
            {
                Bairro = bairro,
                DistritoMunicipal = distrito,
                Nome = nome_feira,
                RegiaoMunicipio5Areas = regiao5
            });

            if (response?.Feiras?.Any() ?? false)
                return Ok(response.Feiras);
            else
                return NoContent();
        }

        /// <summary>
        /// Cria uma nova feira
        /// </summary>
        /// <param name="request">Dados para criação da feira</param>
        /// <returns>Retorna identificador gerado para nova feira</returns>
        /// <response code="200">Executou a operação com sucesso</response>
        /// <response code="400">Houveram falhas de negócio durante o processamento da operação</response>
        /// <response code="500">Ocorreu um erro inesperado durante a execução da operação</response>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AddNewFeiraResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateFeira([FromBody] AddNewFeiraRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Atualiza dados de feira
        /// </summary>
        /// <param name="request">Dados da feira a serem atualizados</param>
        /// <response code="200">Executou a operação com sucesso</response>
        /// <response code="400">Houveram falhas de negócio durante o processamento da operação</response>
        /// <response code="500">Ocorreu um erro inesperado durante a execução da operação</response>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateFeira([FromBody] UpdateFeiraRequest request)
        {
            await Mediator.Send(request);

            return Ok();
        }
    }
}
