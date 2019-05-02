using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.MapObjects
{
    public class Line
    {
        private Point[] points;
        public Line(Point point1, Point point2)
        {
                Points = new Point[2];
                Points[0] = point1;
                Points[1] = point2;

        }
        public Line(Point[] points)
        {
            if(points.Length == 2)
            {
                Points = points;
            }
            else
            {
                Points = new Point [2];
                Points[0] = new Point(0, 0);
                Points[1] = new Point(1, 1);
            }
        }
        
        public Line(int xa, int ya, int xb, int yb)
        {
            Points = new Point[2];
            Points[0] = new Point(xa, ya);
            Points[1] = new Point(xb, yb);
        }

        public Point [] Points
        {
            get
            {
                return points;
            }

            set
            {
                if(value.Length == 2)
                {
                    points = value;
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
