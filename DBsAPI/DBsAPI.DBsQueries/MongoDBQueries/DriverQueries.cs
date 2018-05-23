using System.Collections.Generic;
using System.Collections;
using DBsAPI.Helpers;
using DBsAPI.Model.MongoDBEntities;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
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
            var queryId = new ObjectId(id);
            var filter = Builders<Driver>.Filter.Eq("_id", queryId);
            return await driversCollection.Find(filter).FirstOrDefaultAsync();

        }

        public async Task AddDriver(Driver driver)
        {
            await driversCollection.InsertOneAsync(driver);
        }

        public async Task RemoveDriver(string id)
        {
            var queryId = new ObjectId(id);
            var filter = Builders<Driver>.Filter.Eq("_id", queryId);
            await driversCollection.DeleteOneAsync(filter);
        }

        //public async Task<UpdateResult> UpdateNote(string id, string body)
        //{
        //    var filter = Builders<Driver>.Filter.Eq()
        //    var update = Builders<Driver>.Update
        //                    .Set(s => s.Body, body)
        //                    .CurrentDate(s => s.UpdatedOn);

        //    return await driversCollection.UpdateOneAsync(filter, update);
        //}
    }
}
