using DBsAPI.Helpers;
using Cassandra;
using Cassandra.Data.Linq;
using DBsAPI.Model.CassandraEntities;
using System.Collections.Generic;

namespace DBsAPI.DBsQueries.CassandraQueries
{
    public class HistoryOfInspectionsQueries
    {
        private readonly string uri = DBsConnetctionStrings.Cassandra.uri;

        private ISession _session;

        public HistoryOfInspectionsQueries()
        {
            CassandraEngine cassandraEngine = new CassandraEngine();
            _session = cassandraEngine.GetSession();
        }

        public IEnumerable<HistoryOfInspection> GetHistoryOfInspections()
        {
            var history = new Table<HistoryOfInspection>(_session);

            IEnumerable<HistoryOfInspection> historyOfInspections = history
                .Execute();

            return historyOfInspections;
        }

        public IEnumerable<HistoryOfInspection> GetHistoryOfInspectionsByTram(int tramID)
        {
            var history = new Table<HistoryOfInspection>(_session);

            IEnumerable<HistoryOfInspection> historyOfInspections = history
                .Where(h => h.TramID == tramID)
                .Execute();

            return historyOfInspections;
        }

        public HistoryOfInspection CreateHistoryOfInspection(HistoryOfInspection historyOfInspection)
        {
            var history = new Table<HistoryOfInspection>(_session);

            history.Insert(historyOfInspection);     

            return historyOfInspection;
        }
    }
}
