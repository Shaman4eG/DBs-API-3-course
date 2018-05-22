using DBsAPI.DBsQueries.CassandraQueries;
using DBsAPI.Model.CassandraEntities;
using System.Web.Http;
using System.Collections.Generic;

namespace DBsAPI.API.Controllers.CassandraControllers
{
    [RoutePrefix("api/historyofinspection")]
    public class HistoryOfInspectionController : ApiController
    {
        private readonly HistoryOfInspectionsQueries historyOfInspectionsQueries;

        public HistoryOfInspectionController()
        {
            historyOfInspectionsQueries = new HistoryOfInspectionsQueries();
        }

        [HttpGet]
        [Route("getinspections")]
        public IEnumerable<HistoryOfInspection> GetHistoryOfInspections()
        {
            IEnumerable<HistoryOfInspection> historyOfInspection = historyOfInspectionsQueries.GetHistoryOfInspections();

            return historyOfInspection;
        }

        [HttpGet]
        [Route("getinspections/{tramID:int}")]
        public  IEnumerable<HistoryOfInspection> GetHistoryOfInspectionByTram(int tramID)
        {
            IEnumerable<HistoryOfInspection> historyOfInspection = historyOfInspectionsQueries.GetHistoryOfInspectionsByTram(tramID);

            return historyOfInspection;
        }

        [HttpPost]
        [Route("createinspection")]
        public HistoryOfInspection CreateHistoryOfInspection(HistoryOfInspection historyOfInspection)
        {

            return historyOfInspectionsQueries.CreateHistoryOfInspection(historyOfInspection);
        }
    }
}