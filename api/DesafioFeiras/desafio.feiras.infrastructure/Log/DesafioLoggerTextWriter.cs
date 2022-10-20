using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.Log
{
    internal class DesafioLoggerTextWriter : DesafioLoggerWriterBase
    {
        public DesafioLoggerTextWriter(DesafioLoggerSettings settings, IDesafioFileWriter fileWriter) : base(settings, fileWriter)
        {
        }

        protected override string[] FormatLog(DesafioLogStructure log)
            => new string[] { string.Format("{0:yyyy-MM-dd HH:mm:ss}|{1}|{2} > {3}|{4}", DateTime.Now, log.Id, log.Category, log.Scope, log.Message) };
    }
}
