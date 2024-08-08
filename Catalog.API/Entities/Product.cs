using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("Name")]
        public required string Name { get; set; }
        public required string Category { get; set; }
        public required string Description { get; set; }
        //TODO: implement logic to save/query image in the database?
        public required string Image { get; set; }
        public decimal Price { get; set; }
    }
}
