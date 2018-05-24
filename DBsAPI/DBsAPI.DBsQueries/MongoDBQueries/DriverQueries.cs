using System.Collections.Generic;
using DBsAPI.Helpers;
using DBsAPI.Model.MongoDBEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DBsAPI.DBsQueries.MongoDBQueries
{
    public class DriverQueries
    {
        private readonly string uri = DBsConnetctionStrings.MongoDB.uri;

        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Driver> driversCollection;

        public DriverQueries()
        {
            client = new MongoClient(uri);
            db = client.GetDatabase("TramParkDB");
            driversCollection = db.GetCollection<Driver>("DriverProfile");
        }

        public async Task<IEnumerable<Driver>> ReadAllDrivers()
        {
            return await driversCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Driver> ReadDriver(string id)
        {
            var filter = CreateFilterById(id);
            return await driversCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddDriver(Driver driver)
        {
            await driversCollection.InsertOneAsync(driver);
        }

        public async Task RemoveDriver(string id)
        {
            var filter = CreateFilterById(id);
            await driversCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateDriver(string id, string fieldName, string fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Driver>.Update.Set(fieldName, fieldValue);

            await driversCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateDriver(string id, string fieldName, int? fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Driver>.Update.Set(fieldName, fieldValue);

            await driversCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateDriver(string id, string fieldName, string[] fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Driver>.Update.Set(fieldName, fieldValue);

            await driversCollection.UpdateOneAsync(filter, update);
        }

        private FilterDefinition<Driver> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Driver>.Filter.Eq($"{nameof(Driver._id)}", queryId);
        }
    }
}
