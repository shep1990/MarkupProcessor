using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data
{
    public static class DataStoreExtensions
    {
        public static void AddDataStore(this IServiceCollection services, string applicationName, IConfiguration configuration)
        {
            var cosmosDbEndpoint = configuration["Cosmos:CosmosDbAccountEndpoint"];
            var cosmosDbKey = configuration["Cosmos:CosmosDbAccountKey"];

            services.AddSingleton(s => (new CosmosClientBuilder(cosmosDbEndpoint, cosmosDbKey))
             .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
             .WithApplicationName(applicationName)
             .Build());
        }

    }
}
