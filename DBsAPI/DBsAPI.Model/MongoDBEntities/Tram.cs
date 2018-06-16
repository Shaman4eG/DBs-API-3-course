namespace DBsAPI.Model.MongoDBEntities
{
    using MongoDB.Bson;
    public class Tram
    {
        public class Capacity
        {
            public int sit { get; set; }
            public int stay { get; set; }
            public int disabled { get; set; }
        }

        public ObjectId _id { get; set; }
        public int number{ get; set; }
        public int route { get; set; }
        public string model { get; set; }
        public Capacity capacity { get; set; }
    }
}
