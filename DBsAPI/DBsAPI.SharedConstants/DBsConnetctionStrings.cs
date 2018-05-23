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
    }
}