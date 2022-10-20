using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.Log
{
    public sealed class DesafioLoggerWriterFactory
    {
        private readonly Dictionary<LogFormat, Func<IDesafioLoggerWriter>> logFormatters = new Dictionary<LogFormat, Func<IDesafioLoggerWriter>>();
        private readonly DesafioLoggerSettings settings;
        private readonly IDesafioFileWriter fileWriter;

        public DesafioLoggerWriterFactory(DesafioLoggerSettings settings, IDesafioFileWriter fileWriter)
        {
            this.settings = settings;
            this.fileWriter = fileWriter;

            Inicialize();
        }

        public IDesafioLoggerWriter Factory()
        {
            return logFormatters[settings.Format].Invoke();
        }

        private void Inicialize()
        {
            AddWriter(LogFormat.Json, () => new DesafioLoggerJsonWriter(settings, fileWriter));
            AddWriter(LogFormat.Text, () => new DesafioLoggerTextWriter(settings, fileWriter));
        }

        private void AddWriter(LogFormat format, Func<IDesafioLoggerWriter> func)
        {
            logFormatters[format] = func;
        }
    }
}
