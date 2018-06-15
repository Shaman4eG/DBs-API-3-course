using System;
using DBsAPI.Model.Neo4jEntities;
using System.Web.Http;
using DBsAPI.DBsQueries.DBsQueries.Neo4jQueries;

namespace DBsAPI.API.Controllers.Neo4jControllers
{
    [RoutePrefix("api/stations")]
    public class StationsController : ApiController
    {
        private readonly StationQueries _stationQueries;

        public StationsController()
        {
            _stationQueries = new StationQueries();
        }

        [HttpPost]
        [Route("createstation")]
        public Guid CreateStation(Station station)
        {
            return _stationQueries.CreateStation(station);
        }

        [HttpGet]
        [Route("getstation/{stationId:Guid}")]
        public Station GetStation(Guid stationId)
        {
            return _stationQueries.GetStation(stationId);
        }

        [HttpPut]
        [Route("updatestation")]
        public Station UpdateStation(Station station)
        {
            return _stationQueries.UpdateStation(station);
        }

        [HttpDelete]
        [Route("deletestation/{stationId:Guid}")]
        public void DeleteStation(Guid stationId)
        {
            _stationQueries.DeleteStation(stationId);
        }
    }
}
