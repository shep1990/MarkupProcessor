using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using Microsoft.Azure.Cosmos;

namespace MarkupProcessor.Data.Repositories
{
    public class FlowDiagramInformationRepository : IFlowDiagramInformationRepository   
    {
        private readonly CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;
        const string databaseId = "flow-diagram-env-database";
        const string containerId = "flow-diagram-env-container";

        public FlowDiagramInformationRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<FlowDiagram> Add(FlowDiagram diagramInformation)
        {
            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            _container = await _database.CreateContainerIfNotExistsAsync(containerId, "/id");
            var itemResponse = await _container.CreateItemAsync(diagramInformation, CreatePartitionKey(diagramInformation));
            return itemResponse.Resource;
        }

        private static PartitionKey CreatePartitionKey(FlowDiagram item)
        {
            return new PartitionKey(item.Id.ToString());
        }
    }
}
