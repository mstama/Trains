using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trains.Interfaces;
using Trains.Models;

namespace Trains.Services
{
    /// <summary>
    /// Graph walker
    /// </summary>
    public partial class GraphWalker : IGraphWalker
    {
        private static readonly StringComparer _comparer = StringComparer.OrdinalIgnoreCase;

        /// <summary>
        /// Find all paths between 2 towns (Dijkstra)
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dest"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<string> FindPaths(IGraph graph, string origin, string dest, int limit, EvalOption option)
        {
            List<string> found = new List<string>();
            if (!graph.Towns.ContainsKey(dest)) { return found; }
            if (!graph.Towns.TryGetValue(origin, out Town originTown)) { return found; }

            // Stop condition
            Func<int, int, int, bool> breakFunc = StopOrDistanceSelection(option,BreakSelection(option));
            // Break condition
            Func<int, int, int, bool> acceptFunc = StopOrDistanceSelection(option, AcceptSelection(option));

            Queue<MetaTown> queue = new Queue<MetaTown>();
            queue.Enqueue(new MetaTown(originTown, 0));

            while (queue.Count > 0)
            {
                var previousMeta = queue.Dequeue();
                var previous = previousMeta.Data;
                foreach (var route in previous.Routes.Values)
                {
                    var meta = new MetaTown(route, previousMeta);
                    if (_comparer.Equals(meta.Data.Name, dest) && acceptFunc(meta.Stops, meta.Distance, limit))
                    {
                        found.Add(meta.Breadcrumb());
                    }
                    if (breakFunc(meta.Stops, meta.Distance, limit))
                    {
                        queue.Enqueue(meta);
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// Find all shortest paths from a origin to dest
        /// </summary>
        /// <remarks>Origin and dest can not be the same</remarks>
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

            ShortTown initial = bag.Find(t => _comparer.Equals(t.Data.Name, originTown.Name));
            initial.Distance = 0;

            while (bag.Count > 0)
            {
                // min distance
                ShortTown current = bag.OrderBy(t => t.Distance).FirstOrDefault();

                bag.Remove(current);
                var tuple = Tuple.Create(current.Data.Name, current.Previous?.Name, current.Distance);
                distances.Add(tuple);
                // Exits when find dest
                if (_comparer.Equals(current.Data.Name, dest))
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

                var routes = current.Data.Routes;

                foreach (var route in routes.Values)
                {
                    var target = bag.FirstOrDefault(t => _comparer.Equals(t.Data.Name, route.Destination.Name));
                    // not visited yet
                    if (target != null)
                    {
                        int currentDistance = current.Distance + route.Distance;

                        // Updates distance in bag
                        if (currentDistance < target.Distance)
                        {
                            target.Distance = currentDistance;
                            target.Previous = current.Data;
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

            var initial = bag.Find(t => _comparer.Equals(t.Data.Name, originTown.Name));
            initial.Distance = 0;

            while (bag.Count > 0)
            {
                // min distance
                var current = bag.OrderBy(t => t.Distance).FirstOrDefault();
                bag.Remove(current);
                distances.Add(Tuple.Create(current.Data.Name, current.Previous?.Name, current.Distance));
                var routes = current.Data.Routes;

                foreach (var route in routes.Values)
                {
                    var target = bag.FirstOrDefault(t => _comparer.Equals(t.Data.Name, route.Destination.Name));
                    // not visited yet
                    if (target != null)
                    {
                        int currentDistance = current.Distance + route.Distance;
                        if (currentDistance < target.Distance)
                        {
                            target.Distance = currentDistance;
                            target.Previous = current.Data;
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
            if (names == null || names.Length < 2) { return -1; }
            if (!graph.Towns.TryGetValue(names[0], out Town previous)) { return -1; }
            int total = 0;
            for (int i = 1; i < names.Length; i++)
            {
                var current = names[i];
                if (!previous.Routes.TryGetValue(current, out Route route)) { return -1; }
                total += route.Distance;
                previous = route.Destination;
            }
            return total;
        }

        #region Private

        /// <summary>
        /// # equal limit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool Equal(int value, int limit)
        {
            return value==limit;
        }

        /// <summary>
        /// # less than limit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool Max(int value, int limit)
        {
            return value < limit;
        }

        /// <summary>
        /// # less or equal than limit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static bool MaxEqual(int value, int limit)
        {
            return value <= limit;
        }

        private static Func<int,int,bool> BreakSelection(EvalOption option)
        {
            if (option.HasFlag(EvalOption.Max))
            {
                return Max;
            }
            if (option.HasFlag(EvalOption.MaxEqual))
            {
                return MaxEqual;
            }
            if (option.HasFlag(EvalOption.Equal))
            {
                return MaxEqual;
            }
            return null;
        }

        private static Func<int, int, bool> AcceptSelection(EvalOption option)
        {
            if (option.HasFlag(EvalOption.Max))
            {
                return Max;
            }
            if (option.HasFlag(EvalOption.MaxEqual))
            {
                return MaxEqual;
            }
            if (option.HasFlag(EvalOption.Equal))
            {
                return Equal;
            }
            return null;
        }

        private static Func<int, int, int, bool> StopOrDistanceSelection(EvalOption option, Func<int,int,bool> func)
        {
            if (option.HasFlag(EvalOption.Distance))
            {
                return (stop, distance, limit) => func(distance, limit);
            }
            if (option.HasFlag(EvalOption.Stop))
            {
                return (stop, distance, limit) => func(stop,limit);
            }
            return null;
        }

        #endregion Private
    }
}