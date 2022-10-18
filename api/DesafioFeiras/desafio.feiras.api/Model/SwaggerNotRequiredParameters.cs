using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace desafio.feiras.api.Model
{

    /// <summary>
    /// Atributo utilizado para indicar para o Swagger que um parâmetro não é obrigatório
    /// </summary>
    public class SwaggerNotRequiredParameters : Attribute
    {
        /// <summary>
        /// Nomes dos parâmetros que não são obrigatórios
        /// </summary>
        public string[] Names { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        public SwaggerNotRequiredParameters(params string[] names)
        {
            this.Names = names;
        }
    }

    /// <summary>
    /// Operation Filter utilizado pelo swagger generator identificar parâmetros não obrigatórios
    /// </summary>
    public class NotRequiredParameters : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var notRequiredParameters = context.MethodInfo.GetCustomAttribute<SwaggerNotRequiredParameters>();

            if(notRequiredParameters != null)
            {
                foreach (var param in operation.Parameters)
                    param.Required = !notRequiredParameters.Names.Contains(param.Name);
            }
        }
    }
}
