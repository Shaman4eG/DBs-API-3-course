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

        public async Task UpdateDriver(string id, string name, int? salary, int? expirience, string[] licensedformodels)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Driver>.Update.Set("123", "qwe");
            update.Set("3453", "sda");



            //if (name != null)
            //    var update = updateBuilder.Set($"{nameof(Driver.name)}", name);

            //if (salary != null)
            //    updateBuilder.Set($"{nameof(Driver.salary)}", salary);

            //if (expirience != null)
            //    updateBuilder.Set($"{nameof(Driver.expirience)}", expirience);

            //if (licensedformodels != null)
            //    updateBuilder.Set($"{nameof(Driver.licensedformodels)}", licensedformodels);

            //await driversCollection.UpdateOneAsync(filter, update);
        }

        private FilterDefinition<Driver> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Driver>.Filter.Eq($"{nameof(Driver._id)}", queryId);
        }
    }
}
