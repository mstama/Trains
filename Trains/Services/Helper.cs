// Copyright (c) 2017 Marcos Tamashiro. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace Trains.Services
{
    /// <summary>
    /// Helper class
    /// </summary>
    public static class Helper
    {
        private static readonly char[] _separators = { ',', ' ', '-' };

        /// <summary>
        /// Separate names in a string array
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] ExtractNames(string text)
        {
            return text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}