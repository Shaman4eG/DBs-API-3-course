using DBsAPI.DBsQueries.CassandraQueries;
using DBsAPI.Model.CassandraEntities;
using System.Web.Http;
using System.Collections.Generic;

namespace DBsAPI.API.Controllers.CassandraControllers
{
    [RoutePrefix("api/historyofincident")]
    public class HistoryOfIncidentController : ApiController
    {
        private readonly HistoryOfIncidentQueries historyOfIncidentQueries;

        public HistoryOfIncidentController()
        {
            historyOfIncidentQueries = new HistoryOfIncidentQueries();
        }

        [HttpGet]
        [Route("getincidents")]
        public IEnumerable<HistoryOfIncident> GetHistoryOfIncident()
        {
            IEnumerable<HistoryOfIncident> historyOfIncident = historyOfIncidentQueries.GetHistoryOfIncidents();

            return historyOfIncident;
        }

        [HttpGet]
        [Route("getincidents/{tramID:int}")]
        public IEnumerable<HistoryOfIncident> GetHistoryOfIncidentByTram(int tramID)
        {
            IEnumerable<HistoryOfIncident> historyOfInspection = historyOfIncidentQueries.GetHistoryOfIncidentByTram(tramID);

            return historyOfInspection;
        }

        [HttpPost]
        [Route("createincident")]
        public HistoryOfIncident CreateHistoryOfIncidient(HistoryOfIncident historyOfIncident)
        {

            return historyOfIncidentQueries.CreateHistoryOfIncident(historyOfIncident);
        }
    }
}