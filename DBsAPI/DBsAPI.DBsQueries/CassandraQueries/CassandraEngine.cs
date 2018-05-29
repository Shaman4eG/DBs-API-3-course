using Cassandra;
using DBsAPI.Helpers;

namespace DBsAPI.DBsQueries
{
    public class CassandraEngine
    {
        private Cluster cluster;
        private ISession session;

        public CassandraEngine()
        {
            SetCluster();
        }

        public ISession GetSession()
        {
            if (cluster == null)
            {
                cluster = Connect();
                session = cluster.Connect();
            }
            else if (session == null)
            {
                session = cluster.Connect();
            }

            return session;
        }

        private void SetCluster()
        {
            if (cluster == null)
            {
                cluster = Connect();
            }
        }

        private Cluster Connect()
        {
            var contactPoint = DBsConnetctionStrings.Cassandra.uri;
            var keyspace = DBsConnetctionStrings.Cassandra.keyspace;

            return Cluster.Builder()
                .AddContactPoint(contactPoint)
                .WithDefaultKeyspace(keyspace)
                .Build();
        }
    }
}
