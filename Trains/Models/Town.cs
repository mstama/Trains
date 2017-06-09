using System;
using System.Collections.Generic;
using System.Text;

namespace Trains.Models
{
    public class Town
    {
        public string Name { get; set; }

        public Town(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var target = obj as Town;
            if (target == null) return false;
            return Name == target.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
