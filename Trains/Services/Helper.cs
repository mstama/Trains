using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Services
{
    public static class Helper
    {
        private static char[] _separators = new char[] { ',', ' ','-' };

        public static string[] ExtractNames(string text)
        {
            var names = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            return names;
        }
    }
}
