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
            IsSimulationOn = false;
        }
        public void MakeTimeJump(int timePeriod)
        {
            //todo
        }
        public void ForceEvent(IGameEvent gameEvent)
        {
            //todo
        }
    }
}
