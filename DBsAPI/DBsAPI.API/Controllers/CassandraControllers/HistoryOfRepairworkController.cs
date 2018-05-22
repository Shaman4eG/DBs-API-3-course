using DBsAPI.DBsQueries.CassandraQueries;
using DBsAPI.Model.CassandraEntities;
using System.Web.Http;
using System.Collections.Generic;

namespace DBsAPI.API.Controllers.CassandraControllers
{
    [RoutePrefix("api/historyofrepairwork")]
    public class HistoryOfRepairworkController : ApiController
    {
        private readonly HistoryOfRepairworksQueries historyOfRepairworksQueries;

        public HistoryOfRepairworkController()
        {
            historyOfRepairworksQueries = new HistoryOfRepairworksQueries();
        }

        [HttpGet]
        [Route("getrepairworks")]
        public IEnumerable<HistoryOfRepairwork> GetHistoryOfRepairworks()
        {
            IEnumerable<HistoryOfRepairwork> historyOfRepairwork = historyOfRepairworksQueries.GetHistoryOfRepairworks();

            return historyOfRepairwork;
        }

        [HttpGet]
        [Route("getrepairworks/{tramID:int}")]
        public IEnumerable<HistoryOfRepairwork> GetHistoryOfRepairworkByTram(int tramID)
        {
            IEnumerable<HistoryOfRepairwork> historyOfRepairwork = historyOfRepairworksQueries.GetHistoryOfRepairworkByTram(tramID);

            return historyOfRepairwork;
        }

        [HttpPost]
        [Route("createrepairwork")]
        public HistoryOfRepairwork CreateHistoryOfRepairwork(HistoryOfRepairwork historyOfRepairwork)
        {

            return historyOfRepairworksQueries.CreateHistoryOfRepairwork(historyOfRepairwork);
        }
    }
}