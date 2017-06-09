using System;
using System.Collections.Generic;
using System.Text;
using Trains.Interfaces;

namespace Trains.Models
{
    public class Graph : IGraph
    {
        private List<Town> _towns = new List<Town>();
        private List<Route> _routes = new List<Route>();

        public Route AddRoute(Town origin, Town destination, int distance)
        {
            var route = _routes.Find(r => r.Origin.Equals(origin) && r.Destination.Equals(destination));
            if (route == null) {
                route= new Route(origin, destination, distance);
                _routes.Add(route);
            }
            return route;
        }

        public Town AddTown(string name)
        {
            var town = _towns.Find(t => t.Name == name);
            if (town == null)
            {
                town = new Town(name);
                _towns.Add(town);
            }
            return town;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            foreach(var town in _towns)
            {
                text.AppendFormat("{0}\n", town);
            }
            foreach (var route in _routes)
            {
                text.AppendFormat("{0}\n", route);
            }
            return text.ToString();
        }
    }
}
