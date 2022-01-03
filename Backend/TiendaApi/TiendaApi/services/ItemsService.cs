using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TiendaApi.Models;

namespace TiendaApi.services
{
    public class ItemsService
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public ItemsService(
            IOptions<ItemStoreDatabaseSettings> itemStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                itemStoreDatabaseSettings.Value.ConnectionStrings);

            var mongoDatabase = mongoClient.GetDatabase(
                itemStoreDatabaseSettings.Value.Database);

            _itemsCollection = mongoDatabase.GetCollection<Item>(
                itemStoreDatabaseSettings.Value.ItemsCollectionName);
        }

        public async Task<List<Item>> GetAsync() =>
            await _itemsCollection.Find(_ => true).ToListAsync();

        public async Task<Item?> GetAsync(string id) =>
            await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Item newItem) =>
            await _itemsCollection.InsertOneAsync(newItem);

        public async Task UpdateAsync(string id, Item updatedItem) =>
            await _itemsCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);

        public async Task RemoveAsync(string id) =>
            await _itemsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
