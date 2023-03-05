using MarkupProcessor.Data.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkupProcessor.Data.Repositories
{
    public class MarkupRepository : IMarkupRepository
    {
        private readonly CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;

        public MarkupRepository(CosmosClient cosmosClient) { 
            _cosmosClient = cosmosClient;
            const string databaseId = "markup-processor-database";
            const string containerId = "markup-processor-container";

            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _database.GetContainer(containerId);
        }

        public async Task<MDContents> Add(MDContents contents) {
            var itemResponse = await _container.CreateItemAsync(contents, CreatePartitionKey(contents));
            return itemResponse.Resource;
        }

        private static PartitionKey CreatePartitionKey(MDContents item)
        {
            return new PartitionKey(item.FlowChartId);
        }
    }
}
