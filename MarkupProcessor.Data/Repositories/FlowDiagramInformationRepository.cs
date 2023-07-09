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

        public FlowDiagramInformationRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            const string databaseId = "flow-diagram-env-database";
            const string containerId = "flow-diagram-env-container";

            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _database.GetContainer(containerId);
        }

        public async Task<FlowDiagram> Add(FlowDiagram diagramInformation)
        {
            var itemResponse = await _container.CreateItemAsync(diagramInformation, CreatePartitionKey(diagramInformation));
            return itemResponse.Resource;
        }

        private static PartitionKey CreatePartitionKey(FlowDiagram item)
        {
            return new PartitionKey(item.Id.ToString());
        }
    }
}
