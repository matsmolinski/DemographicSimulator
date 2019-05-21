using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.MapObjects
{
    public class City
    {
        public readonly Point point;
        public readonly string name;
        public City(Point point, int population, string name)
        {
            this.point = point;
            Population = population;
            this.name = name;
            CityData = new CityData();
        }

        public int Population { set; get; }
        public CityData CityData { set; get; }

        public double Distance(Point p)
        {
            return Math.Sqrt(Math.Pow(point.X - p.X, 2) + Math.Pow(point.Y - p.Y, 2));
        }
    }
}
