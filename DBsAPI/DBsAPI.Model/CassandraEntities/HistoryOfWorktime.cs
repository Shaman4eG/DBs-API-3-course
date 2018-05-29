using System;

namespace DBsAPI.Model.CassandraEntities
{
    public class HistoryOfWorktime
    {
        public int WorkerID { get; set; }
        public int TramID { get; set; }
        public string Day { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset Finish { get; set; }
    }
}