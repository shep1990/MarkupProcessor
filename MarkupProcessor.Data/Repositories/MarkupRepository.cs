using MarkupProcessor.Data.Interfaces;
using MarkupProcessor.Data.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace MarkupProcessor.Data.Repositories
{
    public class MarkupRepository : IMarkupRepository
    {
        private readonly CosmosClient _cosmosClient;
        private Database _database;
        private Microsoft.Azure.Cosmos.Container _container;
        private const string databaseId = "flow-diagram-env-database";
        private const string containerId = "markup-processor-container";

        public MarkupRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _database.GetContainer(containerId);
        }

        public async Task<List<MDContents>> Get(string flowchartId)
        {
            var itemResponse = _container.GetItemLinqQueryable<MDContents>().Where(x => x.FlowChartId == flowchartId).ToFeedIterator();
            FeedResponse<MDContents> queryResultSetIterator = await itemResponse.ReadNextAsync();
            return queryResultSetIterator.ToList();
        }

        public async Task<MDContents> Add(MDContents contents)
        {
            var itemResponse = await _container.CreateItemAsync(contents, CreatePartitionKey(contents));
            return itemResponse.Resource;
        }

        private static PartitionKey CreatePartitionKey(MDContents item)
        {
            return new PartitionKey(item.FlowChartId.ToString());
        }
    }
}
