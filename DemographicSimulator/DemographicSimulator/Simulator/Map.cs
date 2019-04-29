using DemographicSimulator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.Simulator
{
    public class Map
    {
        public MapConfiguration mc;

        public Map()
        {
            ContourLines = new List<Line>();
            Rivers = new List<River>();
            Cities = new List<City>();
        }

        public List <Line> ContourLines { set; get; }
        public List <River> Rivers { set; get; }
        public List <City> Cities { set; get; }
    }
}
