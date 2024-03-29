﻿namespace DemographicSimulator.MapObjects
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
            if(obj is Line line)
            {
                return (points[0].Equals(line.points[0]) && points[1].Equals(line.points[1]))
                    || (points[0].Equals(line.points[1]) && points[1].Equals(line.points[0]));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return points[0].GetHashCode() + points[1].GetHashCode() + 456642221;
        }

        public override string ToString()
        {
            return points[0].X + " " + points[0].Y + " " + points[1].X + " " + points[1].Y;
        }
    }
}
