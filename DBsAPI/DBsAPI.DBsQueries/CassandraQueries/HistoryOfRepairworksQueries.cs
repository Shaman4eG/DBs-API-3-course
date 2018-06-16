using DBsAPI.Helpers;
using Cassandra;
using Cassandra.Data.Linq;
using DBsAPI.Model.CassandraEntities;
using System.Collections.Generic;

namespace DBsAPI.DBsQueries.CassandraQueries
{
    public class HistoryOfRepairworksQueries
    {
        private readonly string uri = DBsConnetctionStrings.Cassandra.uri;

        private ISession _session;

        public HistoryOfRepairworksQueries()
        {
            CassandraEngine cassandraEngine = new CassandraEngine();
            _session = cassandraEngine.GetSession();
        }

        public IEnumerable<HistoryOfRepairwork> GetHistoryOfRepairworks()
        {
            var history = new Table<HistoryOfRepairwork>(_session);

            IEnumerable<HistoryOfRepairwork> historyOfRepairwork = history
                .Execute();

            return historyOfRepairwork;
        }

        public IEnumerable<HistoryOfRepairwork> GetHistoryOfRepairworkByTram(int tramID)
        {
            var history = new Table<HistoryOfRepairwork>(_session);

            IEnumerable<HistoryOfRepairwork> historyOfRepairwork = history
                .Where(h => h.TramID == tramID)
                .Execute();

            return historyOfRepairwork;
        }

        public HistoryOfRepairwork CreateHistoryOfRepairwork(HistoryOfRepairwork historyOfRepairwork)
        {
            var history = new Table<HistoryOfRepairwork>(_session);

            history.Insert(historyOfRepairwork);

            return historyOfRepairwork;
        }
    }
}
