using System;

namespace DBsAPI.Model.CassandraEntities
{
    public class HistoryOfRepairwork
    {
        public int TramID { get; set; }
        public DateTimeOffset RepairStarted { get; set; }
        public DateTimeOffset RepairFinished { get; set; }
        public string Description { get; set; }
        public int Expenses { get; set; }
        public string Responsible { get; set; }
    }
}