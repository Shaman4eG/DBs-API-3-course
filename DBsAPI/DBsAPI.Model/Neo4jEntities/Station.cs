using System;
using System.Collections.Generic;

namespace DBsAPI.Model.Neo4jEntities
{
    public class Station
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public List<int> startStation;
        public List<int> endStation;
        public List<int> intermediateStation;
    }
}