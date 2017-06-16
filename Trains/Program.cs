using System;
using System.IO;
using Trains.Interfaces;
using Trains.Models;
using Trains.Services;

namespace Trains
{
    internal class Program
    {
        private static IGraphFactory _factory;
        private static IGraphWalker _graphWalker;
        private static IGraphParser<string, IGraph> _parser;

        private static void CheckDistance(IGraph graph, int number, string route)
        {
            var q = _graphWalker.TotalRouteDistance(graph, Helper.ExtractNames(route));
            Console.WriteLine("Output #{0}:{1}", number, q > 0 ? q.ToString() : "NO SUCH ROUTE");
        }

        private static void FindPaths(IGraph graph, int number, string origin, string dest, int maxStops, PathOption option)
        {
            var r = _graphWalker.FindPaths(graph, origin, dest, maxStops, option);
            Console.WriteLine("Output #{0}:{1}", number, r.Count);
        }

        // Composition root
        private static void Init()
        {
            _factory = new GraphFactory();
            _parser = new GraphParser(_factory);
            _graphWalker = new GraphWalker();
        }

        private static void Main(string[] args)
        {
            Init();

            if (args.Length == 0)
            {
                Console.WriteLine("Input file required!");
                return;
            }
            // Build graph
            string filePath = args[0];
            if (!File.Exists(filePath)) Console.WriteLine("File does not exist!");
            Console.WriteLine("Processing file {0}.", args[0]);
            var lines = File.ReadLines(filePath);
            IGraph graph = null;
            foreach (var line in lines)
            {
                graph = _parser.Parse(line);
            }

            // Q1
            CheckDistance(graph, 1, "A-B-C");

            // Q2
            CheckDistance(graph, 2, "A-D");

            // Q3
            CheckDistance(graph, 3, "A-D-C");

            // Q4
            CheckDistance(graph, 4, "A-E-B-C-D");

            // Q5
            CheckDistance(graph, 5, "A-E-D");

            // Q6
            FindPaths(graph, 6, "C", "C", 3, PathOption.StopMaxEqual);

            // Q7
            FindPaths(graph, 7, "A", "C", 4, PathOption.StopEqual);

            // Q8
            ShortestPathDistance(graph, 8, "A", "C");

            // Q9
            ShortestPathDistanceRT(graph, 9, "B", "B");

            //Q10
            FindPaths(graph, 10, "C", "C", 30, PathOption.DistanceMaxEqual);

            Console.ReadLine();
        }

        private static void ShortestPathDistance(IGraph graph, int number, string origin, string dest)
        {
            var d = _graphWalker.ShortestPathDistance(graph, origin, dest);
            Console.WriteLine("Output #{0}:{1}", number, d);
        }

        private static void ShortestPathDistanceRT(IGraph graph, int number, string origin, string dest)
        {
            var distances = _graphWalker.ShortestPaths(graph, origin);
            int min = int.MaxValue;
            foreach (var distance in distances)
            {
                int returnDist = _graphWalker.ShortestPathDistance(graph, distance.Item1, origin);
                int roundTrip = distance.Item3 + returnDist;
                if (roundTrip > 0 && roundTrip < min)
                {
                    min = roundTrip;
                }
            }
            Console.WriteLine("Output #{0}:{1}", number, min);
        }
    }
}