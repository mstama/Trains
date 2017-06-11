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

        public IList<string> FindRoutes(string originName, string destName, int maxDepth,Func<int,int,bool> depthFunc)
        {
            Town origin = FindTown(originName);
            Town dest = FindTown(destName);
            List<string> found = new List<string>();

            if (origin == null || dest == null) return found;
            Queue<MetaTown> queue = new Queue<MetaTown>();
            queue.Enqueue(new MetaTown(origin,0));

            while (queue.Count > 0)
            {
                var meta = queue.Dequeue();
                var current = meta.TownData;
                // Process current
                int currentDepth = meta.Depth;
                string crumb = meta.Breadcrumb;
                currentDepth++;
                if (currentDepth <= maxDepth) {
                    foreach (var route in current.Routes)
                    {
                        var child = route.Destination;

                        if(child == dest && depthFunc(currentDepth,maxDepth))
                        {
                            found.Add(string.Format("{0}{1}", crumb, child.Name));
                        }
                        queue.Enqueue(new MetaTown(child, currentDepth, crumb));
                    }
                }
            }
            return found;
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