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

        public Guid CreateStation(Station station)
        {
            if (String.IsNullOrWhiteSpace(station?.name)
                || station.startStation == null
                || station.endStation == null
                || station.intermediateStation == null)
            {
                return Guid.Empty;
            }

            station.id = Guid.NewGuid();

            client
                .Cypher
                .Create("(s:Station {station})")
                .WithParam("station", station)
                .ExecuteWithoutResults();

            return station.id;
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

            if (!result.Any()) return null; 

            return new Station()
            {
                id = result.ElementAt(0).id,
                name = result.ElementAt(0).name,
                startStation = result.ElementAt(0).startStation,
                endStation = result.ElementAt(0).endStation,
                intermediateStation = result.ElementAt(0).intermidiateStation
            };
        }

        public Station UpdateStation(Station station)
        {
            if (String.IsNullOrWhiteSpace(station?.name)
                || station.startStation == null
                || station.endStation == null
                || station.intermediateStation == null)
            {
                return null;
            }

            client
                .Cypher
                .Match("(s:Station)")
                .Where((Station s) => s.id == station.id)
                .Set("s = {updatedStation}")
                .WithParam("updatedStation", station)
                .ExecuteWithoutResults();

            return station;
        }

        public void DeleteStation(Guid stationId)
        {
            //client
            //    .Cypher
            //    .Match("(s:Station)")
            //    .Where((Station s) => s.id == stationId)
            //    .DetachDelete("s")
            //    .ExecuteWithoutResults();

            var queryText = "MATCH (s:Station) " +
                            $"WHERE s.id = '{stationId}' " +
                            "DETACH DELETE s";
            CypherQuery query = new CypherQuery(queryText, null, CypherResultMode.Projection, CypherResultFormat.DependsOnEnvironment); 

            ((IRawGraphClient) client).ExecuteCypher(query);
        }
    }
}
