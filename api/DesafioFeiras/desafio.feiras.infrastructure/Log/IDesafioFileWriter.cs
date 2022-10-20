namespace desafio.feiras.infrastructure.Log
{
    public interface IDesafioFileWriter
    {
        void AppendLines(string filepath, string[] lines);
    }
}
