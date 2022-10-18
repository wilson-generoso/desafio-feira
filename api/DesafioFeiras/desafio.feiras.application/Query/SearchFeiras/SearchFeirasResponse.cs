using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.application.Query.SearchFeiras
{
    /// <summary>
    /// Dados de resposta de pesquisa por feiras
    /// </summary>
    public class SearchFeirasResponse
    {
        /// <summary>
        /// Lista de feiras encontradas conforme filtro informado
        /// </summary>
        public IEnumerable<SearchFeiraResponse> Feiras { get; set; }
    }

    /// <summary>
    /// Dados de resposta de feira encontrada
    /// </summary>
    public class SearchFeiraResponse : domain.Feira
    {
    }
}
