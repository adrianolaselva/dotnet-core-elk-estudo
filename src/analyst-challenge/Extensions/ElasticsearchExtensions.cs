using System;
using analyst_challenge.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace analyst_challenge
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ELASTICSEARCH_URL"];
            var defaultIndex = configuration["ELASTICSEARCH_INDEX"];

            var settings = new ConnectionSettings(new Uri(url))
                .DeadTimeout(TimeSpan.FromSeconds(10))
                .DefaultIndex($"{defaultIndex}-{DateTime.Now:yyyyMMdd}")
                .DefaultMappingFor<EventReceiver>(m => m
                    .PropertyName(p => p.Id, "id")
                );
            
            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
        }
    }
}
