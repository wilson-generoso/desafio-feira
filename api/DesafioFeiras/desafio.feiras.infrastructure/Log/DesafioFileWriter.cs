using System.Diagnostics.CodeAnalysis;

namespace desafio.feiras.infrastructure.Log
{
    [ExcludeFromCodeCoverage]
    public class DesafioFileWriter : IDesafioFileWriter
    {
        public void AppendLines(string filepath, string[] lines)
        {
            File.AppendAllLines(filepath, lines);
        }
    }
}
