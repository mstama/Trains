using System;
using System.IO;
using Trains.Interfaces;
using Trains.Services;

namespace Trains
{
    class Program
    {
        private static IGraphParser<string,IGraph> _parser;
        // Composition root
        private static void Init()
        {
            _parser = new GraphParser(new GraphFactory());
        }

        static void Main(string[] args)
        {
            Init();

            if (args.Length == 0)
            {
                Console.WriteLine("Input file required!");
                return;
            }
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
            FindRoutes(graph, 6, "C", "C", 3, (i, j) => i <= j);

            // Q7
            FindRoutes(graph, 7, "A", "C", 4, (i, j) => i == j);

            // Q8
            ShortestPathDistance(graph, 8, "A", "C");

            // Q9
            ShortestPathDistance(graph, 9, "B", "B");

            Console.WriteLine(graph);
            Console.ReadLine();
        }

        private static void CheckDistance(IGraph graph, int number, string route)
        {
            var q = graph.CalculateRouteDistance(Helper.ExtractNames(route));
            Console.WriteLine("Output #{0}:{1}", number, q > 0 ? q.ToString() : "NO SUCH ROUTE");
        }

        private static void FindRoutes(IGraph graph, int number, string origin, string dest, int maxDepth, Func<int,int,bool> funcDepth)
        {
            var r = graph.FindRoutes(origin, dest, maxDepth, funcDepth);
            Console.WriteLine("Output #{0}:{1}", number, r.Count);
        }

        private static void ShortestPathDistance(IGraph graph,int number, string origin, string dest)
        {
            var d = graph.ShortestPathDistance(origin,dest);
            Console.WriteLine("Output #{0}:{1}", number, d);
        }
    }
}