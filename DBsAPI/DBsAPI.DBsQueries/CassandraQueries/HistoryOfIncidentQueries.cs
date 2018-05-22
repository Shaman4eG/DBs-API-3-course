using DBsAPI.Helpers;
using Cassandra;
using Cassandra.Data.Linq;
using DBsAPI.Model.CassandraEntities;
using System.Collections.Generic;

namespace DBsAPI.DBsQueries.CassandraQueries
{
    public class HistoryOfIncidentQueries
    {
        private readonly string uri = DBsConnetctionStrings.Cassandra.uri;

        private ISession _session;

        public HistoryOfIncidentQueries()
        {
            CassandraEngine cassandraEngine = new CassandraEngine();
            _session = cassandraEngine.GetSession();
        }

        public IEnumerable<HistoryOfIncident> GetHistoryOfIncidents()
        {
            var history = new Table<HistoryOfIncident>(_session);

            IEnumerable<HistoryOfIncident> historyOfIncident = history
                .Execute();

            return historyOfIncident;
        }

        public IEnumerable<HistoryOfIncident> GetHistoryOfIncidentByTram(int tramID)
        {
            var history = new Table<HistoryOfIncident>(_session);

            IEnumerable<HistoryOfIncident> historyOfIncident = history
                .Where(h => h.TramID == tramID)
                .Execute();

            return historyOfIncident;
        }

        public HistoryOfIncident CreateHistoryOfIncident(HistoryOfIncident historyOfIncident)
        {
            var history = new Table<HistoryOfIncident>(_session);

            history.Insert(historyOfIncident);

            return historyOfIncident;
        }
    }
}
