using desafio.feiras.infrastructure.Log;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace desafio.feiras.infrastructure.tests
{
    public class DesafioLoggerTest
    {
        [Fact(DisplayName = "Log em formato texto")]
        public void LogText()
        {
            var fileWriterMock = new DesafioFileWriterMock();

            var settings = new DesafioLoggerSettings
            {
                Application = "Teste",
                EventId = 0,
                Format = LogFormat.Text,
                Level = LogLevel.Trace,
                FilePattern = "{Date:yyyyMMddHHmm}_{Application}_Log.txt"
            };

            var providers = new List<ILoggerProvider>();

            using var logProvider = new DesafioLoggerProvider(settings, fileWriterMock);

            providers.Add(logProvider);

            var loggerFactory = new LoggerFactory(providers);
            var logger = loggerFactory.CreateLogger<DesafioLoggerTest>();

            using (logger.BeginScope("Inicio teste"))
            {
                logger.LogInformation("Teste 01");
                logger.LogTrace("Teste Trace 02");
            }

            fileWriterMock.Lines.Count.Should().Be(2);
        }

        [Fact(DisplayName = "Log em formato JSON")]
        public void LogJson()
        {
            var fileWriterMock = new DesafioFileWriterMock();

            var settings = new DesafioLoggerSettings
            {
                Application = "Teste",
                EventId = 0,
                Format = LogFormat.Json,
                Level = LogLevel.Trace,
                FilePattern = "{Date:yyyyMMddHHmm}_{Application}_Log.txt"
            };

            var providers = new List<ILoggerProvider>();

            using var logProvider = new DesafioLoggerProvider(settings, fileWriterMock);

            providers.Add(logProvider);

            var loggerFactory = new LoggerFactory(providers);
            var logger = loggerFactory.CreateLogger<DesafioLoggerTest>();

            using (logger.BeginScope("Inicio teste"))
            {
                logger.LogInformation("Teste 01");
                logger.LogTrace("Teste Trace 02");
            }

            fileWriterMock.Lines.Count.Should().Be(2);
        }
    }
}