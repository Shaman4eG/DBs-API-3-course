using System.Collections.Generic;
using DBsAPI.Helpers;
using DBsAPI.Model.MongoDBEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DBsAPI.DBsQueries.MongoDBQueries
{
    public class ScheduleQueries
    {
        private readonly string uri = DBsConnetctionStrings.MongoDB.uri;

        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Schedule> SchedulesCollection;

        public ScheduleQueries()
        {
            client = new MongoClient(uri);
            db = client.GetDatabase("TramParkDB");
            SchedulesCollection = db.GetCollection<Schedule>("Schedule");
        }

        public async Task<IEnumerable<Schedule>> ReadAllSchedules()
        {
            return await SchedulesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Schedule> ReadSchedule(string id)
        {
            var filter = CreateFilterById(id);
            return await SchedulesCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddSchedule(Schedule Schedule)
        {
            await SchedulesCollection.InsertOneAsync(Schedule);
        }

        public async Task RemoveSchedule(string id)
        {
            var filter = CreateFilterById(id);
            await SchedulesCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateSchedule(string id, string fieldName, string fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Schedule>.Update.Set(fieldName, fieldValue);

            await SchedulesCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateSchedule(string id, string fieldName, int? fieldValue)
        {
            var filter = CreateFilterById(id);
            var update = Builders<Schedule>.Update.Set(fieldName, fieldValue);

            await SchedulesCollection.UpdateOneAsync(filter, update);
        }

        private FilterDefinition<Schedule> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Schedule>.Filter.Eq($"{nameof(Schedule._id)}", queryId);
        }
    }
}
