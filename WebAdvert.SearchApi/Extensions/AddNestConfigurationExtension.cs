using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Elasticsearch.Net.Aws;
using Elasticsearch.Net;
using WebAdvert.SearchApi.Models;

namespace WebAdvert.SearchApi.Extensions
{
    public static class AddNestConfigurationExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticUrl = configuration.GetSection("ES").GetValue<string>("url");
            var awsOptions = configuration.GetAWSOptions();
            //awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();

            var httpConnection = new AwsHttpConnection(awsOptions);
            var pool = new SingleNodeConnectionPool(new Uri(elasticUrl));

            var connectionSettings = new ConnectionSettings(pool, httpConnection)
                .DefaultIndex("adverts")
                .DefaultMappingFor<AdvertType>(advert => advert.IdProperty(p => p.Id));

            var client = new ElasticClient(connectionSettings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
