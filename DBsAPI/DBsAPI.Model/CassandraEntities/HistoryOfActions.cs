using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBsAPI.Model.CassandraEntities
{
    class HistoryOfActions
    {
        public int WorkerID { get; set; }
        public int TramID { get; set; }
        public string Date { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
    }
}
