using DBsAPI.DBsQueries;
using DBsAPI.Model.Neo4jEntities;
using System.Web.Http;

namespace DBsAPI.API.Controllers.Neo4jControllers
{
    [RoutePrefix("api/stations")]
    public class StationsController : ApiController
    {
        private readonly Neo4jQueries _neo4jQueries;

        public StationsController()
        {
            _neo4jQueries = new Neo4jQueries();
        }

        [HttpPost]
        [Route("createstation")]
        public int? CreateStation(Station station)
        {
            var id = _neo4jQueries.CreateStation(station);
            return id;
        }

        [HttpGet]
        [Route("getstation/{id:int}")]
        public Station GetStation(int id)
        {
            var station = _neo4jQueries.GetStation(id);
            return station;
        }
    }
}
