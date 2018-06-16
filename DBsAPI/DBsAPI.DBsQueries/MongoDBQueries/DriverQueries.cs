using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DBsAPI.Helpers;
using DBsAPI.Model.MongoDBEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Bogus;

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

        public long PopulateDrivers(int size)
        {
            var watch = Stopwatch.StartNew();

            //
            // ИЗМЕНИ ПУТЬ ДО НУЖНОГО ТЕБЕ ФАЙЛА
            //
            using (var sw = new StreamWriter(@"C:\Users\vladimir.bakshenov\Downloads\Private\testDriver.json"))
            {
                for (var i = 0; i < size; i++)
                {
                    var fakeDriver = new Faker<Driver>()
                            .RuleFor(d => d.expirience, f => f.Random.Number(1, 40))
                            .RuleFor(d => d.salary, f => f.Random.Number(10000, 50000))
                            .RuleFor(d => d.name, f => f.Name.FullName())
                            .RuleFor(d => d.licensedformodels, f => f.Lorem.Words(i%5+1))
                            .Generate();

                    sw.Write(new JavaScriptSerializer().Serialize(fakeDriver));
                }
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        private FilterDefinition<Driver> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Driver>.Filter.Eq($"{nameof(Driver._id)}", queryId);
        }
    }
}
