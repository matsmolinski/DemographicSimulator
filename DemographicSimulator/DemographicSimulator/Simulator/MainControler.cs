using DemographicSimulator.Events;
using DemographicSimulator.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            double factor = Math.Pow(Map.mc.Birthrate + 1, 1.0/12.0);
            while(timePeriod-- > 0)
            {
                foreach (City c in Map.Cities)
                {
                    c.Population = (int)(c.Population * factor);
                }
            }
            
        }
        public void ForceEvent(IGameEvent gameEvent, Point point, int power)
        {
            gameEvent.ProceedEvent(Map, point, power, out int victims, out int migrants);
            MessageBox.Show("Victims: " + victims + "\nMigrants: " + migrants, gameEvent.ToString(), MessageBoxButtons.OK);
        }

    }
}
