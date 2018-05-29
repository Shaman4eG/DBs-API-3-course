namespace DBsAPI.Helpers
{
    public static class DBsConnetctionStrings
    {
        public static class Neo4j
        {
            public static readonly string uri = "bolt://localhost:7687";
            public static readonly string user = "neo4j";
            public static readonly string password = "neo4jpassword";
        }

        public static class MongoDB
        {
            public static readonly string uri = "mongodb://localhost:27017";
	}

        public static class Cassandra
        {
            public static readonly string uri = "127.0.0.1";
            public static readonly string keyspace = "sbd_course";
        }
    }
}