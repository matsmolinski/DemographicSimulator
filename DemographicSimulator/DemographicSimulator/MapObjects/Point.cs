using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.MapObjects
{
    public class Point
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static Point operator +(Point argument1, Point argument2)
        {
            return new Point(argument1.X + argument2.X, argument1.Y + argument2.Y);
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
    }
}
