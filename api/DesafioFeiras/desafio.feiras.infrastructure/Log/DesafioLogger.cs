using Microsoft.Extensions.Logging;

namespace desafio.feiras.infrastructure.Log
{
    public class DesafioLogger : ILogger
    {
        private readonly string categoryName;
        private readonly LogLevel level;
        private readonly IDesafioLoggerWriter loggerWriter;

        public DesafioLogger(string categoryName, LogLevel level, IDesafioLoggerWriter loggerWriter)
        {
            this.categoryName = categoryName;
            this.level = level;
            this.loggerWriter = loggerWriter;
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => ((int)level) <= ((int)logLevel);

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if(IsEnabled(logLevel))
            {
                var log = new DesafioLogStructure
                {
                    Level = logLevel,
                    Id = eventId,
                    Category = categoryName,
                    Message = formatter(state, exception)
                };

                loggerWriter.Write(log);
            }
        }
    }
    
}
