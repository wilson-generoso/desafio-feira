using System.Text.Json;

namespace desafio.feiras.infrastructure.Log
{
    internal class DesafioLoggerJsonWriter : DesafioLoggerWriterBase
    {
        public DesafioLoggerJsonWriter(DesafioLoggerSettings settings, IDesafioFileWriter fileWriter) : base(settings, fileWriter)
        {
        }

        protected override string[] FormatLog(DesafioLogStructure log)
            => new string[]
            {
                JsonSerializer.Serialize(log, options: new JsonSerializerOptions { WriteIndented = false })
            };
        
    }
}
