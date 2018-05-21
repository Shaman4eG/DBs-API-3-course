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

        public static class Cassandra
        {
            public static readonly string uri = "127.0.0.1";
        }
    }
}