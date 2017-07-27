// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    /// <summary>
    /// Evaluation option
    /// </summary>
    [Flags]
    public enum EvalOptions
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
