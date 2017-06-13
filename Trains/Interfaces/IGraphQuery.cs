using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Interfaces
{
    public interface IGraphQuery
    {
        int CalculateRouteDistance(params string[] names);

        IList<string> FindPaths(string originName, string destName, int limit, PathOption option);

        int ShortestPathDistance(string originName, string destName);
    }
}
