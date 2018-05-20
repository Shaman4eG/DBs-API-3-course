using DBsAPI.Helpers;
using Neo4j.Driver.V1;
using System.Linq;
using DBsAPI.Model.Neo4jEntities;

namespace DBsAPI.DBsQueries
{
    public class Neo4jQueries
    {
        private readonly string uri = DBsConnetctionStrings.Neo4j.uri;
        private readonly string user = DBsConnetctionStrings.Neo4j.user;
        private readonly string password = DBsConnetctionStrings.Neo4j.password;

        private readonly IDriver _driver;

        public Neo4jQueries()
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public int? CreateStation(Station station)
        {
            int? id;

            var validData = CheckStationDataIntegrity(station);
            if (!validData) return null;

            FillStationData(station);

            try
            {
                using (var session = _driver.Session())
                {
                    id = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run($"CREATE (s:{station.Label}) " +
                                            $"SET s.name = '{station.Name}' " +
                                            "RETURN id(s)");
                        return result.Single()[0].As<int>();
                    });
                }
            }
            catch
            {
                id = null;
            }

            return id;
        }

        public Station GetStation(int id)
        {
            Station station = null;

            //try
            //{
                using (var session = _driver.Session())
                {
                    var stationHolder = session.WriteTransaction(tx =>
                    {
                        var result = tx.Run("MATCH (s:Station) " +
                                            $"WHERE id(s) = {id} " +
                                            "RETURN s");
                        return result;
                    });
                    station = ConvertToStation(stationHolder);
                }
            //}
            //catch
            //{
            //    station = null;
            //}

            return station;
        }

        #region Helpers

        private bool CheckStationDataIntegrity(Station station)
        {
            if (string.IsNullOrEmpty(station?.Name)) return false;

            return true;
        }

        private void FillStationData(Station station)
        {
            station.Label = "Station";
        }

        private Station ConvertToStation(IStatementResult stationHolder)
        {
            var a = stationHolder.Select(record => record[0].As<string>()).ToList();

            return null;
        }

        #endregion
    }
}
