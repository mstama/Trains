using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Interfaces;

namespace Trains.Models
{
    public class Graph : IGraph, IGraphQuery
    {
        private static Dictionary<PathOption, Func<int, int, int, bool>> _acceptFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        private static Dictionary<PathOption, Func<int, int, int, bool>> _breakFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        static Graph()
        {
            _breakFunc.Add(PathOption.DistanceMax, DistanceMaxEqual);
            _breakFunc.Add(PathOption.DistanceMaxEqual, DistanceMaxEqual);
            _breakFunc.Add(PathOption.DistanceEqual, DistanceMaxEqual);
            _breakFunc.Add(PathOption.StopMax, StopMaxEqual);
            _breakFunc.Add(PathOption.StopMaxEqual, StopMaxEqual);
            _breakFunc.Add(PathOption.StopEqual, StopMaxEqual);
            _acceptFunc.Add(PathOption.DistanceMax, DistanceMax);
            _acceptFunc.Add(PathOption.DistanceMaxEqual, DistanceMaxEqual);
            _acceptFunc.Add(PathOption.DistanceEqual, DistanceEqual);
            _acceptFunc.Add(PathOption.StopMax, StopMax);
            _acceptFunc.Add(PathOption.StopMaxEqual, StopMaxEqual);
            _acceptFunc.Add(PathOption.StopEqual, StopEqual);
        }

        //TODO: think about indexers
        protected IList<Town> Towns { get; } = new List<Town>();

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

        public IList<string> FindPaths(string originName, string destName, int limit, PathOption option)
        {
            Town origin = FindTown(originName);
            Town dest = FindTown(destName);
            List<string> found = new List<string>();
            Func<int, int, int, bool> breakFunc = _breakFunc[option];
            Func<int, int, int, bool> acceptFunc = _acceptFunc[option];
            if (origin == null || dest == null) return found;
            Queue<MetaTown> queue = new Queue<MetaTown>();
            queue.Enqueue(new MetaTown(origin, 0));

            while (queue.Count > 0)
            {
                var meta = queue.Dequeue();
                var current = meta.TownData;
                // Process current
                int currentDepth = meta.Depth;
                string crumb = meta.Breadcrumb;
                int totalDistance = meta.TotalDistance;
                currentDepth++;
                if (breakFunc(currentDepth, totalDistance, limit))
                {
                    foreach (var route in current.Routes)
                    {
                        var child = route.Destination;

                        if (child == dest && acceptFunc(currentDepth, totalDistance + route.Distance, limit))
                        {
                            found.Add(string.Format("{0}{1}", crumb, child.Name));
                        }
                        queue.Enqueue(new MetaTown(child, currentDepth, crumb, totalDistance + route.Distance));
                    }
                }
            }
            return found;
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

        public int ShortestPathDistance(string originName, string destName)
        {
            Town dest = FindTown(destName);
            var distances = ShortestPaths(originName);
            var distance = distances.FirstOrDefault(s => s.TownData.Name == dest.Name);
            return distance.Distance;
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

        protected IList<ShortTown> ShortestPaths(string originName)
        {
            Town origin = FindTown(originName);
            List<Town> visited = new List<Town>();
            List<ShortTown> distances = new List<ShortTown>();
            Queue<ShortTown> queue = new Queue<ShortTown>();
            queue.Enqueue(new ShortTown(origin, origin, 0));
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visited.Add(current.TownData);
                var routes = current.TownData.Routes.OrderBy(r => r.Distance);

                foreach (var route in routes)
                {
                    var shortDistance = distances.SingleOrDefault(s => s.TownData.Name == route.Destination.Name);
                    if (shortDistance == null)
                    {
                        shortDistance = new ShortTown(route.Destination, current.TownData, route.Distance + current.Distance);
                        distances.Add(shortDistance);
                    }
                    else
                    {
                        if (shortDistance.Distance > route.Distance + current.Distance)
                        {
                            shortDistance.Distance = route.Distance + current.Distance;
                            shortDistance.Previous = current.TownData;
                        }
                    }
                    // Enqueue
                    if (!visited.Contains(route.Destination))
                    {
                        queue.Enqueue(new ShortTown(route.Destination, current.TownData, current.Distance + route.Distance));
                    }
                }
            }
            return distances;
        }

        private static bool DistanceEqual(int stop, int distance, int limit)
        {
            if (distance == limit) return true;
            return false;
        }

        private static bool DistanceMax(int stop, int distance, int limit)
        {
            if (distance < limit) return true;
            return false;
        }

        private static bool DistanceMaxEqual(int stop, int distance, int limit)
        {
            if (distance <= limit) return true;
            return false;
        }

        private static bool StopEqual(int stop, int distance, int limit)
        {
            if (stop == limit) return true;
            return false;
        }

        private static bool StopMax(int stop, int distance, int limit)
        {
            if (stop < limit) return true;
            return false;
        }

        private static bool StopMaxEqual(int stop, int distance, int limit)
        {
            if (stop <= limit) return true;
            return false;
        }
    }
}