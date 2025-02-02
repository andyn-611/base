namespace MyMongoApi;

using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

    public class ProductService
    {
    private readonly IMongoCollection<Product> _products;

    public ProductService(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
    {
        var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _products = database.GetCollection<Product>("Product");
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetProductById(ObjectId id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public async Task UpdateProduct(ObjectId id, Product product)
    {
        //await _products.ReplaceOneAsync(p => p.Id == id, product);
        var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var update = Builders<Product>.Update.Set(p => p.Name, product.Name)
                                             .Set(p => p.Price, product.Price)
                                             .Set(p => p.Stock, product.Stock);           
        _products.UpdateOne(filter, update);

    }

    public async Task DeleteProduct(ObjectId id)
    {
        await _products.DeleteOneAsync(p => p.Id == id);
    }

}
