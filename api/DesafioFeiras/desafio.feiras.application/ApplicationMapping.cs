using AutoMapper;
using desafio.feiras.application.Command.AddNewFeira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.application
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<domain.Feira, AddNewFeiraRequest>().ReverseMap();
        }
    }
}
