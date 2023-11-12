using Ecommers.API.Models;
using EcommersStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EcommersSApi.Services
{
    public class EcommersService
    {
        private readonly IMongoCollection<Product> _booksCollecion;
        public EcommersService(IOptions<EcommenrsSSettings> ecommersDBSetting)
        {
            var mongoClient = new MongoClient(ecommersDBSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(ecommersDBSetting.Value.DatabaseName);
            _booksCollecion = mongoDatabase.GetCollection<Product>(ecommersDBSetting.Value.BooksColectionName);
        }
        public async Task<List<Product>> GetAsync() =>
            await _booksCollecion.Find(_ => true).ToListAsync();
        
        public async Task<List<Product>> GetInCartAsync() =>
            await _booksCollecion.Find(x => x.CountInCart > 0).ToListAsync();
        
        public async Task<Product?> GetAsync(string id) =>
            await _booksCollecion.Find(x => x.Id == id).FirstOrDefaultAsync();
        
        public async Task CreateAsync(Product newTask) =>
            await _booksCollecion.InsertOneAsync(newTask);
        
        public async Task UpdateAsync(string id, Product updatedTask) =>
            await _booksCollecion.ReplaceOneAsync(x => x.Id == id, updatedTask);
        
        public async Task RemoveAsync(string id) =>
            await _booksCollecion.DeleteOneAsync(x => x.Id == id);
    }
}