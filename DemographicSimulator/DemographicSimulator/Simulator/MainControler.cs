using DemographicSimulator.Events;
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
            Random r = new Random();
            int val = r.Next(1, 100);
            Console.WriteLine("żyję"+val);
        }
        public void ForceEvent(IGameEvent gameEvent)
        {
            //todo
        }

    }
}
