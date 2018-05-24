namespace DBsAPI.Model.MongoDBEntities
{
    using MongoDB.Bson;
    public class Schedule
    {
        public ObjectId _id { get; set; }
        public string   stationName{ get; set; }
        public int      tramRoute { get; set; }
        public int      interval { get; set; }
        public string   firstStopTime { get; set; }
        public string   lastStopTime { get; set; }

    }
}
