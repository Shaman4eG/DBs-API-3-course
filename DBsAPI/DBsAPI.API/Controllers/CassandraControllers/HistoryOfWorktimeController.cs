using DBsAPI.DBsQueries.CassandraQueries;
using DBsAPI.Model.CassandraEntities;
using System.Web.Http;
using System.Collections.Generic;

namespace DBsAPI.API.Controllers.CassandraControllers
{
    [RoutePrefix("api/historyofworktime")]
    public class HistoryOfWorktimeController : ApiController
    {
        private readonly HistoryOfWorktimeQueries historyOfWorktimeQueries;

        public HistoryOfWorktimeController()
        {
            historyOfWorktimeQueries = new HistoryOfWorktimeQueries();
        }

        [HttpGet]
        [Route("getworktimes")]
        public IEnumerable<HistoryOfWorktime> GetHistoryOfWorktimes()
        {
            IEnumerable<HistoryOfWorktime> historyOfWorktime = historyOfWorktimeQueries.GetHistoryOfWorktimes();

            return historyOfWorktime;
        }

        [HttpGet]
        [Route("getworktimes/{workerID:int}")]
        public IEnumerable<HistoryOfWorktime> GetHistoryOfWorktimesByWorker(int workerID)
        {
            IEnumerable<HistoryOfWorktime> historyOfWorktime = historyOfWorktimeQueries.GetHistoryOfWorktimesByWorker(workerID);

            return historyOfWorktime;
        }

        [HttpPost]
        [Route("createworktime")]
        public HistoryOfWorktime CreateHistoryOfWorktime(HistoryOfWorktime historyOfWorktime)
        {

            return historyOfWorktimeQueries.CreateHistoryOfWorktime(historyOfWorktime);
        }
    }
}