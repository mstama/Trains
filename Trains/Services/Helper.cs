using System;
using System.Collections.Generic;
using System.Text;
using Trains.Models;

namespace Trains.Services
{
    public static class Helper
    {
        private static char[] _separators = new char[] { ',', ' ','-' };

        public static Town[] ExtractTowns(string text)
        {
            var towns = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            var extracted = new List<Town>();
            foreach(var name in towns)
            {
                extracted.Add(new Town(name));
            }
            return extracted.ToArray();
        }
    }
}
