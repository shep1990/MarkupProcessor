using MarkupProcessor.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace MarkupProcessor.Initialization
{
    public class MarkupProcessorInitializer : IApplicationInitializer
    {
        public async Task Initialize(IServiceProvider provider, IWebHostEnvironment environment)
        {
            using var scope = provider.CreateScope();
            var cosmosConfig = scope.ServiceProvider.GetRequiredService<IOptions<CosmosConfig>>().Value;
            var cosmosClient = scope.ServiceProvider.GetRequiredService<CosmosClient>();

            await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosConfig.Database);
            await cosmosClient.GetDatabase(cosmosConfig.Database).CreateContainerIfNotExistsAsync(cosmosConfig.FlowDiagramContainer, cosmosConfig.FlowDiagramPartition);
            await cosmosClient.GetDatabase(cosmosConfig.Database).CreateContainerIfNotExistsAsync(cosmosConfig.MarkupContainer, cosmosConfig.MarkupPartition);
        }
    }
}
