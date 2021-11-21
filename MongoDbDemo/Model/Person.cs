using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Model
{
    public class Person
    {
        [BsonId]
        public Guid Id {get; set;}
        public string FirstName { get; set; }
        public string LastName {get; set;}
        public string Email {get; set;}
        public Address PrimaryAdress { get; set; }
        [BsonElement("dob")]
        public DateTime DateOfBirth{get; set;}
    }
}