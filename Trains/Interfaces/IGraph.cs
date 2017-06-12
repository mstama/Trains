using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Interfaces
{
    public interface IGraph
    {
        Town AddTown(string name);

        Route AddRoute(string originName, string destName, int distance);

        int CalculateRouteDistance(params string[] names);

        IList<string> FindPaths(string originName, string destName, int limit, PathOption option);

        int ShortestPathDistance(string originName, string destName);
    }
}
