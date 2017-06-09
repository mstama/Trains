using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Interfaces
{
    public interface IGraph
    {
        Town AddTown(string name);

        Route AddRoute(Town origin, Town destination, int distance);

        int CalculateRouteDistance(params Town[] towns);
    }
}
