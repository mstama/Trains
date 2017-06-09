using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Interfaces
{
    public interface IGraphFactory
    {
        IGraph CreateGraph();
    }
}
