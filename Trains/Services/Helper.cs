using System;

namespace Trains.Services
{
    /// <summary>
    /// Helper class
    /// </summary>
    public static class Helper
    {
        private static char[] _separators = { ',', ' ', '-' };

        /// <summary>
        /// Separate names in a string array
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] ExtractNames(string text)
        {
            var names = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            return names;
        }
    }
}