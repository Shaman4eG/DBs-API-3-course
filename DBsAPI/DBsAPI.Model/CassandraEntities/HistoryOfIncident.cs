﻿using System;

namespace DBsAPI.Model.CassandraEntities
{
    public class HistoryOfIncidient
    {
        public int TramID { get; set; }
        public DateTimeOffset Day { get; set; }
        public string Incident { get; set; }
        public string Description { get; set; }
    }
}