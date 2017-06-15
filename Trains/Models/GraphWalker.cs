using System;
using System.Collections.Generic;
using System.Linq;
using Trains.Interfaces;

namespace Trains.Models
{
    public class GraphWalker : IGraphWalker
    {
        private static Dictionary<PathOption, Func<int, int, int, bool>> _acceptFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        private static Dictionary<PathOption, Func<int, int, int, bool>> _breakFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        /// <summary>
        /// Static constructor
        /// </summary>
        static GraphWalker()
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
        /// Find all paths between 2 towns
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<string> FindPaths(IGraph graph, string origin, string dest, int limit, PathOption option)
        {
            Town originTown = graph.Towns[origin];
            Town destDest = graph.Towns[dest];
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
                    foreach (var route in current.Routes.Values)
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
        /// Return towns given a set of names
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public IList<Town> FindTowns(IGraph graph, params string[] names)
        {
            var towns = new List<Town>();
            if (names == null || names.Length == 0) return towns;
            foreach (var name in names)
            {
                if (!graph.Towns.TryGetValue(name, out Town town)) return new List<Town>();
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
        public int ShortestPathDistance(IGraph graph, string origin, string dest)
        {
            Town destTown = graph.Towns[dest];
            var distances = ShortestPaths(graph, origin);
            var distance = distances.FirstOrDefault(s => s.Item1.Name == destTown.Name);
            return distance.Item3;
        }

        /// <summary>
        /// Find all shortest paths from a origin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Tuple<Town, Town, int>> ShortestPaths(IGraph graph, string origin)
        {
            Town originTown = graph.Towns[origin];
            List<Tuple<Town, Town, int>> distances = new List<Tuple<Town, Town, int>>();
            List<ShortTown> bag = new List<ShortTown>();
            foreach (var town in graph.Towns.Values)
            {
                bag.Add(new ShortTown(town, null, int.MaxValue));
            }

            var initial = bag.Find(t => t.TownData.Name == originTown.Name);
            initial.Previous = originTown;
            initial.Distance = 0;

            while (bag.Count > 0)
            {
                // min distance
                var current = bag.OrderBy(t => t.Distance).FirstOrDefault();
                bag.Remove(current);
                distances.Add(Tuple.Create(current.TownData, current.Previous, current.Distance));
                var routes = current.TownData.Routes;

                foreach (var route in routes.Values)
                {
                    var target = bag.FirstOrDefault(t => t.TownData.Name == route.Destination.Name);
                    // not visited yet
                    if (target != null)
                    {
                        int currentDistance = current.Distance + route.Distance;
                        if (currentDistance < target.Distance)
                        {
                            target.Distance = currentDistance;
                            target.Previous = current.TownData;
                        }
                    }
                }
            }
            return distances;
        }

        /// <summary>
        /// Total route distance
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public int TotalRouteDistance(IGraph graph, params string[] names)
        {
            if (names == null || names.Length < 2) return -1;
            if (!graph.Towns.TryGetValue(names[0], out Town previous)) return -1;
            int total = 0;
            for (int i = 1; i < names.Length; i++)
            {
                var current = names[i];
                if (!previous.Routes.TryGetValue(current, out Route route)) return -1;
                total += route.Distance;
                previous = route.Destination;
            }
            return total;
        }

        /// <summary>
        /// Find all shortest paths from a origin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        protected IList<Tuple<Town, Town, int>> ShortestPaths(IGraph graph, string origin, string dest)
        {
            Town originTown = graph.Towns[origin];
            List<Tuple<Town, Town, int>> distances = new List<Tuple<Town, Town, int>>();
            List<ShortTown> bag = new List<ShortTown>();
            foreach (var town in graph.Towns.Values)
            {
                bag.Add(new ShortTown(town, null, int.MaxValue));
            }

            var initial = bag.Find(t => t.TownData.Name == originTown.Name);
            initial.Previous = originTown;
            initial.Distance = 0;

            while (bag.Count > 0)
            {
                // min distance
                var current = bag.OrderBy(t => t.Distance).FirstOrDefault();
                bag.Remove(current);
                distances.Add(Tuple.Create(current.TownData, current.Previous, current.Distance));
                var routes = current.TownData.Routes;

                foreach (var route in routes.Values)
                {
                    var target = bag.FirstOrDefault(t => t.TownData.Name == route.Destination.Name);
                    // not visited yet
                    if (target != null)
                    {
                        int currentDistance = current.Distance + route.Distance;
                        if (currentDistance < target.Distance)
                        {
                            target.Distance = currentDistance;
                            target.Previous = current.TownData;
                        }
                    }
                }
            }
            return distances;
        }

        #region Private

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

        #endregion Private

    }
}