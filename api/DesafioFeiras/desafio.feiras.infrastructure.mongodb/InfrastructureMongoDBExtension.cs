using desafio.feiras.infrastructure;
using desafio.feiras.infrastructure.mongodb.Feira;
using desafio.feiras.infrastructure.mongodb.Repository;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureMongoDBExtension
    {
        public static IServiceCollection AddInfrastructureMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var settings = GetMongoDBSettings();

            if (settings == null)
            {
                settings = new MongoDBSettings();
                configuration.GetSection(MongoDBSettings.CONFIG_KEY).Bind(settings);
            }

            services.AddSingleton(settings);
            services.AddScoped<IFeiraRepository, FeiraRepository>();
            services.AddTransient<IFeiraService, FeiraService>();

            return services;
        }

        private static MongoDBSettings GetMongoDBSettings()
        {
            var settings = Environment.GetEnvironmentVariable("MONGODB_SETTINGS");

            if (string.IsNullOrEmpty(settings))
                return null;
            else
                return JsonSerializer.Deserialize<MongoDBSettings>(settings);
        }
    }
}
