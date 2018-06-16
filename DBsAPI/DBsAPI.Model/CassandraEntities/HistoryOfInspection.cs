namespace DBsAPI.Model.CassandraEntities
{
    public class HistoryOfInspection
    {
        public int TramID { get; set; }
        public string Day { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}