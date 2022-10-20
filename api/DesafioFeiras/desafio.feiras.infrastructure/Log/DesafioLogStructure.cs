using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.Log
{
    public class DesafioLogStructure
    {
        public LogLevel Level { get; set; }
        public EventId Id { get; set; }
        public string Category { get; set; }
        public string Scope { get; set; }
        public string Message { get; set; }
    }
}
