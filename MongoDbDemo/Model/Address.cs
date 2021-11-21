using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Model
{
    public class Address
    {
        [BsonId]
        public Guid Id {get; set;}
        public string StreetAdrress { get; set; }
        public string City {get; set;}
        public string State {get; set;}
        public string ZipCode {get; set;}
    }
}