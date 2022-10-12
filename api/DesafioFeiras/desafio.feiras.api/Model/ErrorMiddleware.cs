using desafio.feiras.application.Validation;
using System.Net;
using System.Text.Json;

namespace desafio.feiras.api.Model
{
    /// <summary>
    /// Middleware utilizado para capturar falhas na execução de operações e formatá-las em um padrão único
    /// </summary>
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// </summary>
        /// <param name="next"></param>
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                ErrorResponse responseModel = null;

                switch (error)
                {
                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel = new ErrorResponse(error.Message);
                        responseModel.Errors = e.Errors;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel = new ErrorResponse("Algo deu errado durante o processamento da operação solicitada.");
                        responseModel.Errors = new string[] { error.Message };
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
