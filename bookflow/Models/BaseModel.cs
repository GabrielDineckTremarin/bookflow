using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace bookflow.Models
{
    public class BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        private DateTime _createdAt;
        public DateTime createdAt
        {
            get
            {
                return !string.IsNullOrEmpty(Id) && ObjectId.TryParse(Id, out var parsedId) ? parsedId.CreationTime : DateTime.Now;
            }
            set
            {
                _createdAt = value;
            }

        }
    }
}