using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Model
{
    [BsonIgnoreExtraElements]
    public class Name
    {
        public string FirstName { get; set; }
        public string LastName {get; set;}
    }
}