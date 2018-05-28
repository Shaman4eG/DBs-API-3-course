using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Bogus;
using DBsAPI.Helpers;
using DBsAPI.Model.MongoDBEntities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DBsAPI.DBsQueries.MongoDBQueries
{
    public class TramQueries
    {
        private readonly string uri = DBsConnetctionStrings.MongoDB.uri;

        private readonly MongoClient client;
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<Tram> TramsCollection;

        public TramQueries()
        {
            client = new MongoClient(uri);
            db = client.GetDatabase("TramParkDB");
            TramsCollection = db.GetCollection<Tram>("Trams");
        }

        public async Task<IEnumerable<Tram>> ReadAllTrams()
        {
            return await TramsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Tram> ReadTram(string id)
        {
            var filter = CreateFilterById(id);
            return await TramsCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddTram(Tram tram)
        {
            await TramsCollection.InsertOneAsync(tram);
        }

        public async Task RemoveTram(string id)
        {
            var filter = CreateFilterById(id);
            await TramsCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateTram(string id, string fieldName, string fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Tram>.Update.Set(fieldName, fieldValue);

            await TramsCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTram(string id, string fieldName, int? fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Tram>.Update.Set(fieldName, fieldValue);

            await TramsCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTram(string id, string fieldName, object fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Tram>.Update.Set(fieldName, fieldValue);

            await TramsCollection.UpdateOneAsync(filter, update);
        }

        public async Task<long> PopulateTrams(int size)
        {
            var collection = new List<Tram>(size);

            var watch = Stopwatch.StartNew();

            using (var sw = new StreamWriter(@"C:\Users\vladimir.bakshenov\Downloads\Private\test.json"))
            {
                for (var i = 0; i < size; i++)
                {
                    var fake = new Faker<Tram>()
                        .RuleFor(t => t.number, f => f.Random.Number(1, 1000))
                        .RuleFor(t => t.model, f => f.Lorem.Word())
                        .RuleFor(t => t.route, f => f.Random.Number(1, 1000))
                        .RuleFor(t => t.capacity, new Faker<Tram.Capacity>()
                            .RuleFor(c => c.disabled, f => f.Random.Number(1, 100))
                            .RuleFor(c => c.sit, f => f.Random.Number(1, 100))
                            .RuleFor(c => c.stay, f => f.Random.Number(1, 100))
                        ).Generate();
                    var json = new JavaScriptSerializer().Serialize(fake);
                    sw.Write(json);

                }
                  
            }

            //await TramsCollection.InsertManyAsync(collection);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            return elapsedMs;
        }

        private FilterDefinition<Tram> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Tram>.Filter.Eq($"{nameof(Tram._id)}", queryId);
        }
    }
}