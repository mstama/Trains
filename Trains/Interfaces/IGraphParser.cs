using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Interfaces
{
    public interface IGraphParser<TSource,TTarget>
    {
        TTarget Parse(TSource input);
    }
}
