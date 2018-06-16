using System;
using System.Collections.Generic;

namespace DBsAPI.Model.Neo4jEntities
{
    public class TramRoute
    {
        public Guid id { get; set; }
        public Guid stationFrom { get; set; }
        public Guid stationTo { get; set; }
        public List<int> travelsThrough;
    }
}