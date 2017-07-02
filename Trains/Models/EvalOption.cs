using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    /// <summary>
    /// Evaluation option
    /// </summary>
    [Flags]
    public enum EvalOption
    {
        /// <summary> None </summary>
        None = 0,
        /// <summary> Equal (==) </summary>
        Equal = 1,
        /// <summary> Max (<) </summary>
        Max = 2,
        /// <summary> Til max (<=) </summary>
        MaxEqual = 4,
        /// <summary> Stop </summary>
        Stop = 16,
        /// <summary> Distance </summary>
        Distance = 32
    }
}
