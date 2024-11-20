using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace bookflow.Models
{
    public class BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        private DateTime _createdAt;
        public DateTime createdAt
        {
            get
            {
                return !string.IsNullOrEmpty(id) && ObjectId.TryParse(id, out var parsedId) ? parsedId.CreationTime : DateTime.Now;
            }
            set
            {
                _createdAt = value;
            }

        }
    }
}