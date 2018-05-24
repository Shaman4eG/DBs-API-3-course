using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using DBsAPI.DBsQueries.MongoDBQueries;
using DBsAPI.Model.MongoDBEntities;

namespace DBsAPI.API.Controllers.MongoDBControllers
{
    [RoutePrefix("api/trams")]
    public class TramsController : ApiController
    {
        private TramQueries tramQueries;
        TramsController()
        {
            tramQueries = new TramQueries();
        }

        [HttpGet]
        public Task<IEnumerable<Tram>> GetAll()
        {
            return tramQueries.ReadAllTrams();
        }

        [Route("{id}")]
        [HttpGet]
        public Task<Tram> Get(string id)
        {
            return tramQueries.ReadTram(id);
        }

        [HttpPost]
        public Task Post(int number, int route, string model, [FromBody] Tram.Capacity capacity)
        {
           return tramQueries.AddTram(new Tram() { number = number, route = route, model = model, capacity = capacity });
        }

        [Route("{id}")]
        [HttpPut]
        public Task Put(string id, int? number, int? route, string model, [FromBody] Tram.Capacity capacity)
        {
            Task task = Task.Delay(0);

            if (number != null)
                task = tramQueries.UpdateTram(id, $"{nameof(number)}", number);

            if (route != null)
                task = tramQueries.UpdateTram(id, $"{nameof(route)}", route);

            if (model != null)
                task = tramQueries.UpdateTram(id, $"{nameof(model)}", model);

            if (capacity != null)
                task = tramQueries.UpdateTram(id, $"{nameof(capacity)}", capacity);

            return task;
        }

        [Route("{id}")]
        [HttpDelete]
        public Task Delete(string id)
        {
            return tramQueries.RemoveTram(id);
        }
    }
}
