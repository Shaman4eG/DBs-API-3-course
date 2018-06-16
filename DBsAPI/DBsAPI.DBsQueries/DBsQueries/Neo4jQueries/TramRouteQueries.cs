using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using DBsAPI.Helpers;
using DBsAPI.Model.Neo4jEntities;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace DBsAPI.DBsQueries.DBsQueries.Neo4jQueries
{
    public class TramRouteQueries
    {
        private readonly string uri = DBsConnetctionStrings.Neo4j.uri;
        private readonly string user = DBsConnetctionStrings.Neo4j.user;
        private readonly string password = DBsConnetctionStrings.Neo4j.password;

        private readonly IGraphClient _client;
        private readonly StationQueries _stationQueries;

        public TramRouteQueries()
        {
            _client = new GraphClient(new Uri(uri), user, password);
            _client.Connect();
            _stationQueries = new StationQueries();
        }

        public Guid CreateTramRoute(TramRoute tramRoute, Guid idForUpdate = new Guid())
        {
            if (tramRoute?.travelsThrough == null 
                || _stationQueries.GetStation(tramRoute.stationFrom) == null
                || _stationQueries.GetStation(tramRoute.stationTo) == null)
            {
                return Guid.Empty;
            }

            tramRoute.id = idForUpdate == Guid.Empty
                ? Guid.NewGuid()
                : idForUpdate;

            _client
                .Cypher
                .Match("(sFrom:Station)", "(sTo:Station)")
                .Where((Station sFrom) => sFrom.id == tramRoute.stationFrom)
                .AndWhere((Station sTo) => sTo.id == tramRoute.stationTo)
                .Create("(sFrom)-[:HAS_PATH_TO {tramRoute}]->(sTo)")
                .WithParam("tramRoute", tramRoute)
                .ExecuteWithoutResults();

            return tramRoute.id;
        }

        public TramRoute GetTramRoute(Guid tramRouteId)
        {
            var result = _client
                .Cypher
                .Match("()-[t:HAS_PATH_TO]->()")
                .Where((TramRoute t) => t.id == tramRouteId)
                .Return((id, stationFrom, stationTo, travelsThrough) => new
                    {
                        id = Return.As<Guid>("t.id"),
                        stationFrom = Return.As<Guid>("t.stationFrom"),
                        stationTo = Return.As<Guid>("t.stationTo"),
                        travelsThrough = Return.As<List<int>>("t.travelsThrough"),
                    }
                ).Results.ToList();

            if (!result.Any()) return null;

            return new TramRoute
            {
                id = result.ElementAt(0).id,
                stationFrom = result.ElementAt(0).stationFrom,
                stationTo = result.ElementAt(0).stationTo,
                travelsThrough = result.ElementAt(0).travelsThrough
            };
        }

        public TramRoute UpdateTramRoute(TramRoute tramRoute)
        {
            if (tramRoute?.travelsThrough == null
                || _stationQueries.GetStation(tramRoute.stationFrom) == null
                || _stationQueries.GetStation(tramRoute.stationTo) == null)
            {
                return null;
            }

            DeleteTramRoute(tramRoute.id);
            CreateTramRoute(tramRoute, tramRoute.id);

            return tramRoute;
        }

        public void DeleteTramRoute(Guid tramRouteId)
        {
            var queryText = "MATCH (:Station)-[tRoute:HAS_PATH_TO]->(:Station) " +
                            $"WHERE tRoute.id = '{tramRouteId}' " +
                            "DELETE tRoute";
            CypherQuery query = new CypherQuery(queryText, null, CypherResultMode.Projection, CypherResultFormat.DependsOnEnvironment);

            ((IRawGraphClient)_client).ExecuteCypher(query);
        }



        #region DataGenerator

        /* Получим 2.000.002 элементов с данными. 
         * Станций: 668.886 
         * Путей сообщения: 1.333.334
         * */
        private const int NumberOfIterations = 668886;

        private Random randomsGenerator = new Random();

        private Guid currentStationFrom;
        private Guid currentStationTo;

        public void GenerateData()
        {
            currentStationFrom = _stationQueries.CreateStation(GenerateStation());

            for (int i = 0; i < NumberOfIterations; i++)
            {
                currentStationTo = _stationQueries.CreateStation(GenerateStation());
                CreateTramRoute(GenerateTramRoute());
                CreateTramRoute(GenerateTramRoute());
                currentStationFrom = currentStationTo;
            }
        }

        private Station GenerateStation()
        {
            return new Station
            {
                name = "s",
                startStation = GenerateTrams(),
                endStation = GenerateTrams(),
                intermediateStation = GenerateTrams()
            };
        }

        private List<int> GenerateTrams()
        {
            int numberOfTrams = randomsGenerator.Next(0, 2);

            var trams = new List<int>();

            for (int i = 0; i < numberOfTrams; i++)
            {
                trams.Add(randomsGenerator.Next(1, 100000));
            }

            return trams;
        }

        private TramRoute GenerateTramRoute()
        {
            return new TramRoute()
            {
                stationFrom = currentStationFrom,
                stationTo = currentStationTo,
                travelsThrough = GenerateTrams()
            };
        }

        #endregion
    }
}
