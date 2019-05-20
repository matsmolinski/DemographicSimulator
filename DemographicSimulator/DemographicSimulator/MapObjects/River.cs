using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.MapObjects
{
    public class River
    {
        public readonly Line[] riverSegments;

        public River(Line[] riverSegments)
        {
            this.riverSegments = riverSegments;
        }

        public double Distance(Point point)
        {
            double shortestDistance = double.MaxValue;
            foreach(Line line in riverSegments)
            {
                Point p1 = line.Points[0];
                Point p2 = line.Points[1];
                PointF closest;
                float dx = p2.X - p1.X;
                float dy = p2.Y - p1.Y;               
                float t = ((point.X - p1.X) * dx + (point.Y - p1.Y) * dy) /
                    (dx * dx + dy * dy);
                if (t < 0)
                {
                    closest = new PointF(p1.X, p1.Y);
                    dx = point.X - p1.X;
                    dy = point.Y - p1.Y;
                }
                else if (t > 1)
                {
                    closest = new PointF(p2.X, p2.Y);
                    dx = point.X - p2.X;
                    dy = point.Y - p2.Y;
                }
                else
                {
                    closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                    dx = point.X - closest.X;
                    dy = point.Y - closest.Y;
                }
                double distance = Math.Sqrt(dx * dx + dy * dy);
            
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                }
            }
            return shortestDistance;
        }
    }
}
