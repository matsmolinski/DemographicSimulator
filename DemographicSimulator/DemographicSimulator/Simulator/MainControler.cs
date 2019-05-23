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
            //earthquake
            Random r = new Random();
            if(r.Next(1, 100) == 100)
            {
                int cityNo = r.Next(1, Map.Cities.Count + 1);
                int power = r.Next(20, 80);
                int distanceX = r.Next(0, 10);
                int distanceY = r.Next(0, 10);
                ForceEvent(new Earthquake(), Map.Cities[cityNo - 1].point + new Point(distanceX, distanceY), power);
            }
            //drought
            else if(r.Next(1,150) + Map.mc.AvgTemperature > 150)
            {
                int cityNo = r.Next(1, Map.Cities.Count + 1);
                int power = r.Next(20, 100);
                int distanceX = r.Next(0, 15);
                int distanceY = r.Next(0, 15);
                ForceEvent(new Drought(), Map.Cities[cityNo - 1].point + new Point(distanceX, distanceY), power);
            }
            //fire
            else if(r.Next(1, 20) + Map.mc.AvgTemperature / 5 > 18)
            {
                int cityNo = r.Next(1, Map.Cities.Count + 1);
                int power = r.Next(0, 80);
                int distanceX = r.Next(0, 3);
                int distanceY = r.Next(0, 3);
                ForceEvent(new Fire(), Map.Cities[cityNo - 1].point + new Point(distanceX, distanceY), power);
            }
            //wind
            else if(r.Next(1, 3000) + Map.mc.AvgHeight > 3000)
            {
                City highestCity = null;
                double max_height = 0;
                foreach (City c in Map.Cities)
                {
                    if (c.CityData.Height > max_height)
                    {
                        max_height = c.CityData.Height;
                        highestCity = c;
                    }
                }
                int power = r.Next(0, 100);
                int distanceX = r.Next(0, 10);
                int distanceY = r.Next(0, 10);
                ForceEvent(new Wind(), highestCity.point + new Point(distanceX, distanceY), power);
            }
            //flood
            else
            {
                foreach(City c in Map.Cities)
                {
                    if(r.Next(0, 1000) - c.CityData.DistanceToRiver > 800)
                    {
                        int power = r.Next(0, 100);
                        ForceEvent(new Flood(), c.point, power);
                        break;
                    }
                }
            }

        }
        public void ForceEvent(IGameEvent gameEvent, Point point, int power)
        {
            gameEvent.ProceedEvent(Map, point, power, out int victims, out int migrants);
            MessageBox.Show("Point:" + point.ToString() + "\nVictims: " + victims + "\nMigrants: " + migrants, gameEvent.ToString(), MessageBoxButtons.OK);
        }

    }
}
