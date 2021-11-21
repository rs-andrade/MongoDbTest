using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.Data
{
    public class MongoCrud
    {
        private readonly IMongoDatabase _databaseMongo;

        public MongoCrud(string database)
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://rodrigoandrade:batata@cluster0.crgzj.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            _databaseMongo = client.GetDatabase(database);            
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = _databaseMongo.GetCollection<T>(table);

            collection.InsertOne(record);
        }

        public IEnumerable<T> LoadRecords<T>(string table)
        {
            var collection = _databaseMongo.GetCollection<T>(table);

            return collection.Find(new BsonDocument(), new FindOptions{AllowPartialResults=true}).ToEnumerable();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = _databaseMongo.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public IEnumerable<T> LoadRecordByName<T>(string table, string firstName, string lastName)
        {
            var collection = _databaseMongo.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("FirstName", firstName) &
                Builders<T>.Filter.Eq("LastName", lastName);

            return collection.Find(filter).ToEnumerable();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = _databaseMongo.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = collection.ReplaceOne(
                filter,
                record,
                new ReplaceOptions {IsUpsert = true}
            );            
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = _databaseMongo.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = collection.DeleteOne(filter);            
        }
    }

}