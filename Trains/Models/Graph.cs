using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Interfaces;

namespace Trains.Models
{
    public class Graph : IGraph
    {
        private List<Town> _towns = new List<Town>();

        //TODO: think about indexers
        protected IList<Town> Towns { get => _towns; }

        public Route AddRoute(string originName, string destName, int distance)
        {
            var origin = AddTown(originName);
            var route = origin.Routes.FirstOrDefault(r => r.Destination.Name == destName);
            if (route == null)
            {
                route = new Route(origin, AddTown(destName), distance);
            }
            return route;
        }

        public Town AddTown(string name)
        {
            var town = FindTown(name);
            if (town == null)
            {
                town = new Town(name);
                Towns.Add(town);
            }
            return town;
        }

        public int CalculateRouteDistance(params string[] names)
        {
            var towns = FindTowns(names);
            if (towns.Count < 2) return -1;
            var previous = towns[0];
            int total = 0;
            for (int i = 1; i < towns.Count; i++)
            {
                var current = towns[i];
                var route = previous.Routes.FirstOrDefault(r => r.Destination.Name == current.Name);
                if (route == null) return -1;
                total += route.Distance;
                previous = current;
            }
            return total;
        }

        public Town FindTown(string name)
        {
            var town = Towns.FirstOrDefault(t => t.Name == name);
            return town;
        }

        public IList<Town> FindTowns(params string[] names)
        {
            if (names == null) return new List<Town>();
            var towns = new List<Town>();
            foreach (var name in names)
            {
                var town = FindTown(name);
                if (town == null) return new List<Town>();
                towns.Add(town);
            }
            return towns;
        }

        public IList<string> FindRoutes(string originName, string destName, int depth,Func<int,bool> depthFunc)
        {
            Town origin = FindTown(originName);
            Town dest = FindTown(destName);

            Queue<Town> queue = new Queue<Town>();
            Queue<int> depthQueue = new Queue<int>();
            queue.Enqueue(origin);
            depthQueue.Enqueue(0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                // Process current
                int currentDepth = depthQueue.Dequeue();
                Console.WriteLine("--{0}", current);

                foreach (var route in current.Routes)
                {
                    var child = route.Destination;
                    
                    queue.Enqueue(child);
                    depthQueue.Enqueue(currentDepth++);
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            foreach (var town in Towns)
            {
                text.AppendFormat("{0}\n", town);
            }
            foreach (var town in Towns)
            {
                foreach (var route in town.Routes)
                {
                    text.AppendFormat("{0}{1}\n", town, route);
                }
            }
            return text.ToString();
        }
    }
}