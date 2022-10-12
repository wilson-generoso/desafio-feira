using desafio.feiras.api.Model;
using desafio.feiras.application.Command.AddNewFeira;
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
        /// Obtem conta por ID (Identificador gerado automaticamente)
        /// </summary>
        /// <param name="id">Identificador da conta</param>
        /// <returns>Informações da conta pesquisada</returns>
        /// <response code="200">Executou a operação com sucesso</response>
        /// <response code="204">Executou a operação com sucesso, porém não encontrou resultados</response>
        /// <response code="500">Ocorreu um erro inesperado durante a execução da operação</response>
        //[HttpGet("{id}")]
        //[ProducesResponseType(200, Type = typeof(GetAccountsResponse))]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(500, Type = typeof(ErrorResponse))]
        //public async Task<IActionResult> GetAccounts(Guid? id)
        //{
        //    var response = await Mediator.Send(new GetAccountsRequest { Id = id });

        //    if (response.Accounts?.Any() ?? false)
        //        return Ok(response.Accounts);
        //    else
        //        return NoContent();
        //}

        ///// <summary>
        ///// Obtem todas as contas cadastradas
        ///// </summary>
        ///// <returns>Lista de contas cadastradas</returns>
        ///// <response code="200">Executou a operação com sucesso</response>
        ///// <response code="204">Executou a operação com sucesso, porém não encontrou resultados</response>
        ///// <response code="500">Ocorreu um erro inesperado durante a execução da operação</response>
        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(GetAccountsResponse))]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(500, Type = typeof(ErrorResponse))]
        //public async Task<IActionResult> GetAccounts()
        //{
        //    var response = await Mediator.Send(new GetAccountsRequest());

        //    if (response.Accounts?.Any() ?? false)
        //        return Ok(response.Accounts);
        //    else
        //        return NoContent();
        //}

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
        public async Task<IActionResult> CreateAccount([FromBody] AddNewFeiraRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Atualiza dados de conta
        /// </summary>
        /// <param name="request">Dados da conta a serem atualizados</param>
        /// <response code="200">Executou a operação com sucesso</response>
        /// <response code="400">Houveram falhas de negócio durante o processamento da operação</response>
        /// <response code="500">Ocorreu um erro inesperado durante a execução da operação</response>
        //[HttpPut]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400, Type = typeof(ErrorResponse))]
        //[ProducesResponseType(500, Type = typeof(ErrorResponse))]
        //public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        //{
        //    await Mediator.Send(request);

        //    return Ok();
        //}
    }
}
