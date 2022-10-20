using desafio.feiras.infrastructure.Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Microsoft.Extensions.Logging.Abstractions
{
    [ExcludeFromCodeCoverage]
    public static class DesafioLoggerExtension
    {
        public const string LOGGER_SETTINGS = "DesafioLogger";

        public static ILoggingBuilder AddDesafioLogger(this ILoggingBuilder builder, IConfiguration configuration)
        {
            var settings = GetDesafioLoggerSettings();

            if (settings == null)
            {
                settings = new DesafioLoggerSettings();
                configuration.GetSection(LOGGER_SETTINGS).Bind(settings);
            }

            builder.Services.AddSingleton(settings);
            builder.Services.AddTransient<IDesafioFileWriter, DesafioFileWriter>();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, DesafioLoggerProvider>());

            return builder;
        }

        private static DesafioLoggerSettings GetDesafioLoggerSettings()
        {
            var settings = Environment.GetEnvironmentVariable("LOGGER_SETTINGS");

            if (string.IsNullOrEmpty(settings))
                return null;
            else
                return JsonSerializer.Deserialize<DesafioLoggerSettings>(settings);
        }
    }
}
