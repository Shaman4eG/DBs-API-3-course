namespace DBsAPI.Model.MongoDBEntities
{
    using MongoDB.Bson;
    public class Driver
    {
        public ObjectId _id{ get; set; }
        public string name{ get; set; }
        public int expirience { get; set; }
        public int salary{ get; set; }
        public string[] licensedformodels { get; set; }

    }
}
