using MongoDB.Bson;

namespace MyMongoApi
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
