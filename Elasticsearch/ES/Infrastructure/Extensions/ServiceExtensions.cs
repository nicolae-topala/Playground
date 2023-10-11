using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ES.Common.Models;

namespace ES.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection SetupElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new ElasticsearchClientSettings(new Uri(configuration["ElasticsearchSettings:Uri"]))
                .CertificateFingerprint(configuration["ElasticsearchSettings:Fingerprint"])
                .Authentication(new BasicAuthentication(configuration["ElasticsearchSettings:Username"], configuration["ElasticsearchSettings:Password"]));

            settings.DefaultMappingFor<Product>(p => p.IndexName(configuration["ElasticsearchIndecies:ProductsIndex"]));

            var client = new ElasticsearchClient(settings);
            services.AddSingleton(client);

            return services;
        }
    }
}
