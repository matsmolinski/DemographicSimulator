using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.MapObjects
{
    public class Line
    {
        public Line(Point[] points)
        {
            if(points.Length == 2)
            {
                Points = points;
            }
            else
            {
                points = new Point [2];
                points[0] = new Point(0, 0);
                points[1] = new Point(1, 1);
            }
        }

        public Point [] Points
        {
            get
            {
                return Points;
            }

            set
            {
                if(value.Length == 2)
                {
                    Points = value;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Line line &&
                   EqualityComparer<Point[]>.Default.Equals(Points, line.Points);
        }

        public override int GetHashCode()
        {
            return 480822998 + EqualityComparer<Point[]>.Default.GetHashCode(Points);
        }
    }
}
