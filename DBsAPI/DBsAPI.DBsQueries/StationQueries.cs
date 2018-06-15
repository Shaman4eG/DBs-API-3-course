using System;
using System.Collections;
using System.Collections.Generic;
using DBsAPI.Helpers;
using Neo4j.Driver.V1;
using System.Linq;
using DBsAPI.Model.Neo4jEntities;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace DBsAPI.DBsQueries
{
    public class StationQueries
    {
        private readonly string uri = DBsConnetctionStrings.Neo4j.uri;
        private readonly string user = DBsConnetctionStrings.Neo4j.user;
        private readonly string password = DBsConnetctionStrings.Neo4j.password;

        private readonly IGraphClient client;

        public StationQueries()
        {
            client = new GraphClient(new Uri(uri), user, password);
            client.Connect();
        }

        public int? CreateStation(Station station)
        {
            int? id = 100500;

            //var validData = CheckStationDataIntegrity(station);
            //if (!validData) return null;

            //FillStationData(station);

            //try
            //{
            //    using (var session = _driver.Session())
            //    {
            //        id = session.WriteTransaction(tx =>
            //        {
            //            var result = tx.Run($"CREATE (s:{station.Label}) " +
            //                                $"SET s.name = '{station.Name}' " +
            //                                "RETURN id(s)");
            //            return result.Single()[0].As<int>();
            //        });
            //    }
            //}
            //catch
            //{
            //    id = null;
            //}

            return id;
        }

        public Station GetStation(Guid stationId)
        {
            var result = client
                .Cypher
                .Match("(s:Station)")
                .Where((Station s) => s.id == stationId)
                .Return((id, name, startStation, endStation, intermidiateStation) => new
                    {
                        id = Return.As<Guid>("s.id"),
                        name = Return.As<string>("s.name"),
                        startStation = Return.As<List<int>>("s.startStation"),
                        endStation = Return.As<List<int>>("s.endStation"),
                        intermidiateStation = Return.As<List<int>>("s.intermidiateStation")
                    }
                ).Results.ToList();

            if (!result.Any()) return new Station(); 

            return new Station()
            {
                id = result.ElementAt(0).id,
                name = result.ElementAt(0).name,
                startStation = result.ElementAt(0).startStation,
                endStation = result.ElementAt(0).endStation,
                intermediateStation = result.ElementAt(0).intermidiateStation
            };
        }

        #region Helpers

        //private bool CheckStationDataIntegrity(Station station)
        //{
        //    if (string.IsNullOrEmpty(station?.Name)) return false;

        //    return true;
        //}

        //private void FillStationData(Station station)
        //{
        //    station.Label = "Station";
        //}

        private Station ConvertToStation(IEnumerable stationHolder)
        {


            return null;
        }

        #endregion
    }
}
