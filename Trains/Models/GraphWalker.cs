using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Interfaces;

namespace Trains.Models
{
    /// <summary>
    /// Graph walker
    /// </summary>
    public partial class GraphWalker : IGraphWalker
    {
        private static Dictionary<PathOption, Func<int, int, int, bool>> _acceptFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        private static Dictionary<PathOption, Func<int, int, int, bool>> _breakFunc = new Dictionary<PathOption, Func<int, int, int, bool>>();

        private StringComparer _comparer = StringComparer.OrdinalIgnoreCase;

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
            List<string> found = new List<string>();
            if (!graph.Towns.ContainsKey(origin) || !graph.Towns.ContainsKey(dest)) return found;
            Town originTown = graph.Towns[origin];

            // Stop condition
            Func<int, int, int, bool> breakFunc = _breakFunc[option];
            // Break condition
            Func<int, int, int, bool> acceptFunc = _acceptFunc[option];

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

                        if (_comparer.Equals(child.Name, dest) && acceptFunc(currentStops, totalDistance + route.Distance, limit))
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
        /// Find all shortest paths from a origin to dest
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public Tuple<string, int> ShortestPath(IGraph graph, string origin, string dest)
        {
            Town originTown = graph.Towns[origin];
            List<Tuple<string, string, int>> distances = new List<Tuple<string, string, int>>();
            List<ShortTown> bag = new List<ShortTown>();
            foreach (var town in graph.Towns.Values)
            {
                bag.Add(new ShortTown(town, null, int.MaxValue));
            }

            ShortTown initial = bag.Find(t => _comparer.Equals(t.TownData.Name, originTown.Name));
            initial.Distance = 0;

            while (bag.Count > 0)
            {
                // min distance
                ShortTown current = bag.OrderBy(t => t.Distance).FirstOrDefault();

                bag.Remove(current);
                var tuple = Tuple.Create(current.TownData.Name, current.Previous?.Name, current.Distance);
                distances.Add(tuple);
                if (_comparer.Equals(current.TownData.Name, dest))
                {
                    StringBuilder path = new StringBuilder();
                    string previous = tuple.Item2;
                    int dist = tuple.Item3;
                    while (!string.IsNullOrWhiteSpace(previous))
                    {
                        path.Insert(0, tuple.Item1);
                        tuple = distances.Find(t => _comparer.Equals(t.Item1, previous));
                        previous = tuple.Item2;
                    }
                    path.Insert(0, tuple.Item1);
                    return Tuple.Create(path.ToString(), dist);
                }

                var routes = current.TownData.Routes;

                foreach (var route in routes.Values)
                {
                    var target = bag.FirstOrDefault(t => _comparer.Equals(t.TownData.Name, route.Destination.Name));
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
            return Tuple.Create("", 0);
        }

        /// <summary>
        /// Return shortest path distance between 2 towns
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public int ShortestPathDistance(IGraph graph, string origin, string dest)
        {
            var distance = ShortestPath(graph, origin, dest);
            return distance.Item2;
        }

        /// <summary>
        /// Find all shortest paths from a origin
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Tuple<string, string, int>> ShortestPaths(IGraph graph, string origin)
        {
            Town originTown = graph.Towns[origin];
            List<Tuple<string, string, int>> distances = new List<Tuple<string, string, int>>();
            List<ShortTown> bag = new List<ShortTown>();
            foreach (var town in graph.Towns.Values)
            {
                bag.Add(new ShortTown(town, null, int.MaxValue));
            }

            var initial = bag.Find(t => _comparer.Equals(t.TownData.Name, originTown.Name));
            initial.Distance = 0;

            while (bag.Count > 0)
            {
                // min distance
                var current = bag.OrderBy(t => t.Distance).FirstOrDefault();
                bag.Remove(current);
                distances.Add(Tuple.Create(current.TownData.Name, current.Previous.Name, current.Distance));
                var routes = current.TownData.Routes;

                foreach (var route in routes.Values)
                {
                    var target = bag.FirstOrDefault(t => _comparer.Equals(t.TownData.Name, route.Destination.Name));
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