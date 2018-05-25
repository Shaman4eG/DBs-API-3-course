using DBsAPI.Helpers;
using Cassandra;
using Cassandra.Data.Linq;
using DBsAPI.Model.CassandraEntities;
using System.Collections.Generic;

namespace DBsAPI.DBsQueries.CassandraQueries
{
    public class HistoryOfWorktimeQueries
    {
        private readonly string uri = DBsConnetctionStrings.Cassandra.uri;

        private ISession _session;

        public HistoryOfWorktimeQueries()
        {
            CassandraEngine cassandraEngine = new CassandraEngine();
            _session = cassandraEngine.GetSession();
        }

        public IEnumerable<HistoryOfWorktime> GetHistoryOfWorktimes()
        {
            var history = new Table<HistoryOfWorktime>(_session);

            IEnumerable<HistoryOfWorktime> historyOfWorktime = history
                .Execute();

            return historyOfWorktime;
        }

        public IEnumerable<HistoryOfWorktime> GetHistoryOfWorktimesByWorker(int workerID)
        {
            var history = new Table<HistoryOfWorktime>(_session);

            IEnumerable<HistoryOfWorktime> historyOfWorktime = history
                .Where(h => h.WorkerID == workerID)
                .Execute();

            return historyOfWorktime;
        }

        public HistoryOfWorktime CreateHistoryOfWorktime(HistoryOfWorktime historyOfWorktime)
        {
            var history = new Table<HistoryOfWorktime>(_session);

            history.Insert(historyOfWorktime);

            return historyOfWorktime;
        }
    }
}
