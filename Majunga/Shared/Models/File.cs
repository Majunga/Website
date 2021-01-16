using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Majunga.Shared.Models
{
    public class File
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
        public string ShareLink { get; set; }
    }
}
