﻿namespace DBsAPI.Helpers
{
    public static class DBsConnetctionStrings
    {
        public static class Neo4j
        {
            public static readonly string uri = "http://localhost:7474/db/data";//"bolt://localhost:7687";
            public static readonly string user = "neo4j";
            public static readonly string password = "neo4jpassword";
        }
    }
}