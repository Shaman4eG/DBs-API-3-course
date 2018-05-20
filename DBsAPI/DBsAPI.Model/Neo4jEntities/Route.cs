using System.Collections.Generic;

namespace DBsAPI.Model.Neo4jEntities
{
    public class Route
    {
        public string Label { get; set; }
        public int ID { get; set; }
        public List<int> TravelsThrough;
    }
}