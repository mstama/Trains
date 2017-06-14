using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Interfaces;

namespace Trains.Models
{
    /// <summary>
    /// Represents a graph
    /// </summary>
    public class Graph : IGraph, IGraphQuery
    {
        private static Dictionary<PathOption, Func<int, int, int, bool>> _acceptFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        private static Dictionary<PathOption, Func<int, int, int, bool>> _breakFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        /// <summary>
        /// Static constructor
        /// </summary>
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

        /// <summary>
        /// Towns registered
        /// </summary>
        public IList<Town> Towns { get; } = new List<Town>();

        /// <summary>
        /// Add or retrieve a route if exists
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Route AddRoute(string origin, string dest, int distance)
        {
            var originTown = AddTown(origin);
            var route = originTown.Routes.FirstOrDefault(r => r.Destination.Name == dest);
            if (route == null)
            {
                route = new Route(originTown, AddTown(dest), distance);
            }
            else
            {
                if (route.Distance != distance) route.Distance = distance;
            }
            return route;
        }

        /// <summary>
        /// Add or retrieve a town
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Total route distance
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public int TotalRouteDistance(params string[] names)
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

        /// <summary>
        /// Find all paths between 2 towns
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<string> FindPaths(string origin, string dest, int limit, PathOption option)
        {
            Town originTown = FindTown(origin);
            Town destDest = FindTown(dest);
            List<string> found = new List<string>();
            // Stop condition
            Func<int, int, int, bool> breakFunc = _breakFunc[option];
            // Break condition
            Func<int, int, int, bool> acceptFunc = _acceptFunc[option];
            if (originTown == null || destDest == null) return found;
            Queue<MetaTown> queue = new Queue<MetaTown>();
            queue.Enqueue(new MetaTown(originTown, 0));

            while (queue.Count > 0)
            {
                var meta = queue.Dequeue();
                var current = meta.TownData;
                // Process current
                int currentStops = meta.Stops;
                string crumb = meta.Breadcrumb;
                int totalDistance = meta.TotalDistance;
                currentStops++;
                if (breakFunc(currentStops, totalDistance, limit))
                {
                    foreach (var route in current.Routes)
                    {
                        var child = route.Destination;

                        if (child == destDest && acceptFunc(currentStops, totalDistance + route.Distance, limit))
                        {
                            found.Add(string.Format("{0}{1}", crumb, child.Name));
                        }
                        queue.Enqueue(new MetaTown(child, currentStops, crumb, totalDistance + route.Distance));
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// Return a town given a name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Town FindTown(string name)
        {
            var town = Towns.FirstOrDefault(t => t.Name == name);
            return town;
        }

        /// <summary>
        /// Return towns given a set of names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return shortest path distance between 2 towns
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public int ShortestPathDistance(string origin, string dest)
        {
            Town destTown = FindTown(dest);
            var distances = ShortestPaths(origin);
            var distance = distances.FirstOrDefault(s => s.TownData.Name == destTown.Name);
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

        /// <summary>
        /// Distance equal limit
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="distance"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool DistanceEqual(int stop, int distance, int limit)
        {
            if (distance == limit) return true;
            return false;
        }

        /// <summary>
        /// Distance less than limit
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="distance"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool DistanceMax(int stop, int distance, int limit)
        {
            if (distance < limit) return true;
            return false;
        }

        /// <summary>
        /// Distance less or equal limit
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="distance"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool DistanceMaxEqual(int stop, int distance, int limit)
        {
            if (distance <= limit) return true;
            return false;
        }

        /// <summary>
        /// # Stops equal limit
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="distance"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool StopEqual(int stop, int distance, int limit)
        {
            if (stop == limit) return true;
            return false;
        }

        /// <summary>
        /// # Stops less than limit
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="distance"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool StopMax(int stop, int distance, int limit)
        {
            if (stop < limit) return true;
            return false;
        }

        /// <summary>
        /// # Stops less or equal than limit
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="distance"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool StopMaxEqual(int stop, int distance, int limit)
        {
            if (stop <= limit) return true;
            return false;
        }
    }
}