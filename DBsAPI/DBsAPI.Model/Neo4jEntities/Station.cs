using System.Collections.Generic;

namespace DBsAPI.Model.Neo4jEntities
{
    public class Station
    {
        public string Label { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public List<int> StartStation;
        public List<int> EndStation;
    }
}