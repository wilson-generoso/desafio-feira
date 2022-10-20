using Microsoft.Extensions.Logging;

namespace desafio.feiras.infrastructure.Log
{
    public class DesafioLoggerSettings
    {
        public EventId EventId { get; set; } = 0;
        public LogLevel Level { get; set; } = LogLevel.Information;
        public string Application { get; set; } = string.Empty;
        public string FilePattern { get; set; } = "{Date:yyyyMMddHHmm}_{Application}_Log.txt";
        public LogFormat Format { get; set; } = LogFormat.Text;
    }

    public enum LogFormat
    {
        Json,
        Text
    }
}
