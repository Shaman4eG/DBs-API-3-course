using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using DBsAPI.DBsQueries.MongoDBQueries;
using DBsAPI.Model.MongoDBEntities;

namespace DBsAPI.API.Controllers.MongoDBControllers
{
    [RoutePrefix("api/schedule")]
    public class ScheduleController : ApiController
    {
        private ScheduleQueries scheduleQueries;
        ScheduleController()
        {
            scheduleQueries = new ScheduleQueries();
        }

        [HttpGet]
        public Task<IEnumerable<Schedule>> GetAll()
        {
            return scheduleQueries.ReadAllSchedules();
        }

        [Route("{id}")]
        [HttpGet]
        public Task<Schedule> Get(string id)
        {
            return scheduleQueries.ReadSchedule(id);
        }

        [HttpPost]
        public Task Post(Schedule schedule)
        {
            return scheduleQueries.AddSchedule(schedule);
        }

        [Route("{id}")]
        [HttpPut]
        public Task Put(string id, string stationName, int? tramRoute, int? interval, string firstStopTime, string lastStopTime)
        {
            Task task = Task.Delay(0);

            if (stationName != null)
                task = scheduleQueries.UpdateSchedule(id, $"{nameof(stationName)}", stationName);

            if (tramRoute != null)
                task = scheduleQueries.UpdateSchedule(id, $"{nameof(tramRoute)}", tramRoute);

            if (interval != null)
                task = scheduleQueries.UpdateSchedule(id, $"{nameof(interval)}", interval);

            if (firstStopTime != null)
                task = scheduleQueries.UpdateSchedule(id, $"{nameof(firstStopTime)}", firstStopTime);

            if (lastStopTime != null)
                task = scheduleQueries.UpdateSchedule(id, $"{nameof(lastStopTime)}", lastStopTime);

            return task;
        }

        [Route("{id}")]
        [HttpDelete]
        public Task Delete(string id)
        {
            return scheduleQueries.RemoveSchedule(id);
        }

        [Route("populate")]
        [HttpGet]
        public long Populate(int size)
        {
            return scheduleQueries.PopulateSchedule(size);
        }

    }
}
