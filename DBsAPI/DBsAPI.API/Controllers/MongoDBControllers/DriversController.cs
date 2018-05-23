using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBsAPI.Model.MongoDBEntities;
using DBsAPI.DBsQueries.MongoDBQueries;
using System.Threading.Tasks;

namespace DBsAPI.API.Controllers.MongoDBControllers
{
    [RoutePrefix("api/drivers")]
    public class DriversController : ApiController
    {
        private DriverQueries driverQueries;
        DriversController()
        {
            driverQueries = new DriverQueries();
        }

        [HttpGet]
        public Task<IEnumerable<Driver>> GetAll()
        {
            return driverQueries.ReadAllDrivers();
        }

        [Route("{id}")]
        [HttpGet]
        public Task<Driver> Get(string id)
        {
            return driverQueries.ReadDriver(id);
        }

        [HttpPost]
        public Task Post(string name, int expirience, int salary,[FromBody] string[] licensedformodels)
        {
           return driverQueries.AddDriver(new Driver() { name = name, expirience = expirience, salary = salary, licensedformodels = licensedformodels });
        }

        [Route("{id}")]
        [HttpPut]
        public Task Put(string id, string name, int? salary, int? expirience, [FromBody] string[] licensedformodels)
        {
            if (name == null && salary == null && expirience == null && licensedformodels == null)
                return new Task(() => { });
            return driverQueries.UpdateDriver(id, name, salary, expirience, licensedformodels);
        }

        [Route("{id}")]
        [HttpDelete]
        public Task Delete(string id)
        {
            return driverQueries.RemoveDriver(id);
        }
    }
}
