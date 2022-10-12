using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace desafio.feiras.api.Model
{
    /// <summary>
    /// Objeto de suporte para controladores de API
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator mediator;

        /// <summary>
        /// Objeto Mediator, utilizado para execução das ações CQRS (Command Query Responsability Segregation)
        /// </summary>
        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
