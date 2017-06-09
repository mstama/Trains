using System;
using System.Collections.Generic;
using System.Text;
using Trains.Interfaces;
using System.Linq;

namespace Trains.Models
{
    public class Graph : IGraph, IGraphCalculator
    {
        private List<Town> _towns = new List<Town>();
        private List<Route> _routes = new List<Route>();

        protected IList<Route> Routes { get => _routes; }
        protected IList<Town> Towns { get => _towns; }

        public Route AddRoute(Town origin, Town destination, int distance)
        {
            var route = Routes.FirstOrDefault(r => r.Origin.Equals(origin) && r.Destination.Equals(destination));
            if (route == null) {
                route= new Route(origin, destination, distance);
                Routes.Add(route);
            }
            return route;
        }

        public Town AddTown(string name)
        {
            var town = Towns.FirstOrDefault(t => t.Name == name);
            if (town == null)
            {
                town = new Town(name);
                Towns.Add(town);
            }
            return town;
        }

        public int CalculateRouteDistance(params Town[] towns)
        {
            if (towns == null || towns.Length < 2) return -1;
            Town previous = towns[0];
            int total = 0;
            for(int i = 1; i < towns.Length; i++)
            {
                var route = Routes.FirstOrDefault(r => r.Origin.Equals(previous) && r.Destination.Equals(towns[i]));
                if (route == null) return -1;
                total += route.Distance;
                previous = towns[i];
            }
            return total;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            foreach(var town in Towns)
            {
                text.AppendFormat("{0}\n", town);
            }
            foreach (var route in Routes)
            {
                text.AppendFormat("{0}\n", route);
            }
            return text.ToString();
        }
    }
}
