using DemographicSimulator.Events;
using DemographicSimulator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.Simulator
{
    public class MainControler
    {      

        public bool IsSimulationOn { set; get;}
        public Map Map { set; get; }

        public MainControler()
        {
            Map = new Map();
            IsSimulationOn = false;
        }
        public void MakeTimeJump(int timePeriod)
        {
            foreach(City c in Map.Cities)
            {
                c.Population += (int)(Map.mc.Birthrate * c.Population * timePeriod / 12);
            }
        }
        public void ForceEvent(IGameEvent gameEvent)
        {
            //todo
        }

    }
}
