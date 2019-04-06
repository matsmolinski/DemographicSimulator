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

        public City(Point point, int population)
        {
            this.point = point;
            Population = population;
            CityData = new CityData();
        }

        public int Population { set; get; }
        public CityData CityData { set; get; }
    }
}
