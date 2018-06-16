using System;
using System.Web.Http;
using DBsAPI.DBsQueries.DBsQueries.Neo4jQueries;
using DBsAPI.Model.Neo4jEntities;

namespace DBsAPI.API.Controllers.Neo4jControllers
{
    [RoutePrefix("api/tramsroutes")]
    public class TramsRoutesController : ApiController
    {
        private readonly TramRouteQueries _tramRouteQueries;

        public TramsRoutesController()
        {
            _tramRouteQueries = new TramRouteQueries();
        }

        [HttpPost]
        [Route("createtramroute")]
        public Guid CreateTramRoute(TramRoute tramRoute)
        {
            return _tramRouteQueries.CreateTramRoute(tramRoute);
        }

        [HttpGet]
        [Route("gettramroute/{tramRouteId:Guid}")]
        public TramRoute GetStation(Guid tramRouteId)
        {
            return _tramRouteQueries.GetTramRoute(tramRouteId);
        }

        [HttpPut]
        [Route("updatetramroute")]
        public TramRoute UpdateTramRoute(TramRoute tramRoute)
        {
            return _tramRouteQueries.UpdateTramRoute(tramRoute);
        }

        [HttpDelete]
        [Route("deletetramroute/{tramRouteId:Guid}")]
        public void DeleteTramRoute(Guid tramRouteId)
        {
            _tramRouteQueries.DeleteTramRoute(tramRouteId);
        }



        #region DataGenerator

        [HttpPost]
        [Route("generatedata")]
        public void GenerateData()
        {
            _tramRouteQueries.GenerateData();
        }

        #endregion
    }
}