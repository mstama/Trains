// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Trains.Interfaces;

namespace Trains.Models
{
    /// <summary>
    /// Represents a graph
    /// </summary>
    public class Graph : IGraph
    {
        /// <summary>
        /// Towns registered
        /// </summary>
        public IDictionary<string, Town> Towns { get; } = new Dictionary<string, Town>(StringComparer.OrdinalIgnoreCase);

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
            if (!originTown.Routes.TryGetValue(dest, out Route route))
            {
                route = new Route(originTown, AddTown(dest), distance);
            }
            else
            {
                route.Distance = distance;
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
            if (!Towns.TryGetValue(name, out Town town))
            {
                town = new Town(this, name);
            }
            return town;
        }

        public override string ToString()
        {
            var text = new StringBuilder();
            foreach (var town in Towns)
            {
                text.AppendFormat("{0}\n", town.ToString());
            }
            foreach (var town in Towns.Values)
            {
                foreach (var route in town.Routes.Values)
                {
                    text.AppendFormat("{0}{1}\n", town, route);
                }
            }
            return text.ToString();
        }
    }
}