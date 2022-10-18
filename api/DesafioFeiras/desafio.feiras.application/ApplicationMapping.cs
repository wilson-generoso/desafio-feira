using AutoMapper;
using desafio.feiras.application.Command.AddNewFeira;
using desafio.feiras.application.Query.SearchFeiras;

namespace desafio.feiras.application
{
    /// <summary>
    /// Configuração de mapeamentos automáticos da camada de aplicação
    /// </summary>
    public class ApplicationMapping : Profile
    {
        /// <summary>
        /// Configura os mapeamentos
        /// </summary>
        public ApplicationMapping()
        {
            CreateMap<domain.Feira, AddNewFeiraRequest>().ReverseMap();
            CreateMap<domain.Feira, SearchFeiraResponse>().ReverseMap();
        }
    }
}
