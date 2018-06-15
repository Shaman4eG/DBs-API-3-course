using DBsAPI.DBsQueries;
using System;
using System.Web.Http;

namespace DBsAPI.Controllers.Neo4jControllers
{
    [RoutePrefix("api/tramsroutes")]
    public class TramsRoutesController : ApiController
    {
        private readonly StationQueries _stationQueries;

        public TramsRoutesController()
        {
            _stationQueries = new StationQueries();
        }

        [Route("createroute")]
        [HttpPost]
        public int CreateRoute()
        {
            throw new NotImplementedException();
        }
    }
}