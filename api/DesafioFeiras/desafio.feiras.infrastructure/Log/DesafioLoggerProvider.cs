using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.Log
{
    public sealed class DesafioLoggerProvider : ILoggerProvider
    {
        private readonly DesafioLoggerSettings settings;
        private readonly DesafioLoggerWriterFactory writerFactory;
        private readonly ConcurrentDictionary<string, DesafioLogger> loggers = new(StringComparer.OrdinalIgnoreCase);

        public DesafioLoggerProvider(DesafioLoggerSettings settings, IDesafioFileWriter fileWriter)
        {
            this.settings = settings;
            this.writerFactory = new DesafioLoggerWriterFactory(this.settings, fileWriter);
        }

        public ILogger CreateLogger(string categoryName) 
            => loggers.GetOrAdd(categoryName, (category) => new DesafioLogger(category, settings.Level, writerFactory.Factory()));
                                    
        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
