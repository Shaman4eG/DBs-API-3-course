using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using DBsAPI.Model.MongoDBEntities;
using DBsAPI.DBsQueries.MongoDBQueries;

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
        public Task Post(Driver driver)
        {
           return driverQueries.AddDriver(driver);
        }

        [Route("{id}")]
        [HttpPut]
        public Task Put(string id, string name, int? salary, int? expirience, [FromBody] string[] licensedformodels)
        {
            Task task = Task.Delay(0);
           
            if (name != null)
                 task = driverQueries.UpdateDriver(id, $"{nameof(name)}", name);

            if (salary != null)
                task = driverQueries.UpdateDriver(id, $"{nameof(salary)}", salary);

            if (expirience != null)
                task = driverQueries.UpdateDriver(id, $"{nameof(expirience)}", expirience);

            if (licensedformodels != null)
                task = driverQueries.UpdateDriver(id, $"{nameof(licensedformodels)}", licensedformodels);

            return task;
        }

        [Route("{id}")]
        [HttpDelete]
        public Task Delete(string id)
        {
            return driverQueries.RemoveDriver(id);
        }
    }
}
