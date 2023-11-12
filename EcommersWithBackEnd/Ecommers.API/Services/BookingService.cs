using Ecommers.API.Models;
using EcommersStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EcommersSApi.Services
{
    public class BookingServie
    {
        private readonly IMongoCollection<Product> _cartColection;
        public BookingServie(IOptions<EcommenrsSSettings> ecommersDBSetting)
        {
            var mongoClient = new MongoClient(ecommersDBSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(ecommersDBSetting.Value.DatabaseName);
            _cartColection = mongoDatabase.GetCollection<Product>(ecommersDBSetting.Value.CartColectionName)
            ;
        }
        public async Task<List<Product>> GetAsync() =>
            await _cartColection.Find(_ => true).ToListAsync();
        
        public async Task CreateAsync(Product newProduct) =>
            await _cartColection.InsertOneAsync(newProduct);
        
        public async Task RemoveAsync(string id) =>
            await _cartColection.DeleteOneAsync(x => x.Id == id);
    }
}