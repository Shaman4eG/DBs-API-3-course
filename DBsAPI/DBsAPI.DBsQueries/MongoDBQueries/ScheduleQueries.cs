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

        public long PopulateSchedule(int size)
        {
            var watch = Stopwatch.StartNew();

            //
            // ИЗМЕНИ ПУТЬ ДО НУЖНОГО ТЕБЕ ФАЙЛА
            //
            using (var sw = new StreamWriter(@"C:\Users\vladimir.bakshenov\Downloads\Private\testSchedule.json"))
            {
                for (var i = 0; i < size; i++)
                {
                    var fakeSchedule = new Faker<Schedule>()
                        .RuleFor(s => s.tramRoute, f => f.Random.Number(1, 1000))
                        .RuleFor(s => s.interval, f => f.Random.Number(1,60))
                        .RuleFor(s => s.stationName, f => f.Lorem.Word())
                        .RuleFor(s => s.firstStopTime, f => f.Date.Soon(1).ToShortTimeString())
                        .RuleFor(s => s.lastStopTime, f => f.Date.Soon(1).ToShortTimeString())
                        .Generate();

                    sw.Write(new JavaScriptSerializer().Serialize(fakeSchedule));
                }
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        private FilterDefinition<Schedule> CreateFilterById(string id)
        {
            var queryId = new ObjectId(id);
            return Builders<Schedule>.Filter.Eq($"{nameof(Schedule._id)}", queryId);
        }
    }
}
