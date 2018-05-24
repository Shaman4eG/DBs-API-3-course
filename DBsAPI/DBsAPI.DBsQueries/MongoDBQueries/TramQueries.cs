using System.Collections.Generic;
using DBsAPI.Helpers;
using DBsAPI.Model.MongoDBEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DBsAPI.DBsQueries.MongoDBQueries
{
    public class TramQueries
    {
        private readonly string uri = DBsConnetctionStrings.MongoDB.uri;

        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Tram> TramsCollection;

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

        private FilterDefinition<Tram> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Tram>.Filter.Eq($"{nameof(Tram._id)}", queryId);
        }
    }
}
