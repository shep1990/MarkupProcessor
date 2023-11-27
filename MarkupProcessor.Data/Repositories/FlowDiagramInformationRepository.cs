using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

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
            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _database.GetContainer(containerId);
        }

        public async Task<FlowDiagram> Add(FlowDiagram diagramInformation)
        {
            var itemResponse = await _container.CreateItemAsync(diagramInformation, CreatePartitionKey(diagramInformation));
            return itemResponse.Resource;
        }

        public async Task<List<FlowDiagram>> Get()
        {
            var itemResponseFeed = _container.GetItemLinqQueryable<FlowDiagram>().ToFeedIterator();
            FeedResponse<FlowDiagram> queryResultSet = await itemResponseFeed.ReadNextAsync();
            return queryResultSet.ToList();
        }

        private static PartitionKey CreatePartitionKey(FlowDiagram item)
        {
            return new PartitionKey(item.Id.ToString());
        }
    }
}
