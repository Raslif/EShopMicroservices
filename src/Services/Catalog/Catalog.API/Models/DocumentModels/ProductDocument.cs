using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models.DocumentModels
{
    public class ProductDocument
    {
        [BsonId]
        public ObjectId Id {  get; set; }
        public string Name { get; set; } = default!;
        public List<string> Category { get; set; } = [];
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}
