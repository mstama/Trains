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
            Console.WriteLine(graph);
            Console.ReadLine();
        }
    }
}