// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Trains.Interfaces
{
    /// <summary>
    /// Parser interface
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IGraphParser<in TSource,out TTarget>
    {
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        TTarget Parse(TSource input);
    }
}