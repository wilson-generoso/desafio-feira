namespace desafio.feiras.api.Model
{
    /// <summary>
    /// Estrutura de resposta para operações com falha
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponse(string message)
        {
            Message = message;
            Errors = new List<string>();
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        public ErrorResponse(string message, IEnumerable<string> errors)
        {
            Message = message;
            Errors = new List<string>(errors);
        }

        /// <summary>
        /// Mensagem geral para o solicitante
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Lista de falhas encontradas na operação
        /// </summary>
        public IEnumerable<string> Errors { get; set; }
    }
}
