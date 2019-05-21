using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;

namespace DemographicSimulator.Events
{
    class Drought : IGameEvent
    {
        public void ProceedEvent(Map map, Point center, int power, out int victims, out int migrants)
        {
            List<City> safeCities = new List<City>();
            List<City> endangeredCities = new List<City>();

            foreach (City c in map.Cities)
            {
                if (c.Distance(center) >= 300)
                {
                    safeCities.Add(c);
                }
                else
                {
                    endangeredCities.Add(c);
                }
            }
            victims = migrants = 0;
            foreach (City c in endangeredCities)
            {
                Random r = new Random();
                double factor = ((300 - c.Distance(center)) / 300) * power / 100;
                Console.WriteLine(factor);
                int cityVictims = (int)(200 * factor / Math.Pow(map.mc.DevelopmentLevel,2) * (0.6 + r.NextDouble()) / 1.6);
                int cityMigrants = (int)(10000 * factor / Math.Pow(map.mc.DevelopmentLevel, 2) * (0.7 + r.NextDouble()) / 1.7);
                if (c.Population < 10000 / Math.Pow(map.mc.DevelopmentLevel, 2))
                {
                    cityMigrants = (int)(c.Population * factor);
                    cityVictims = 0;
                    c.Population -= cityMigrants;
                }
                else
                {
                    c.Population -= cityVictims;
                    c.Population -= cityMigrants;
                }
                double minDistance = double.MaxValue;
                City closestCity = null;
                foreach (City sc in safeCities)
                {
                    if (c.Distance(sc.point) < minDistance)
                    {
                        minDistance = c.Distance(sc.point);
                        closestCity = sc;
                    }
                }
                if (closestCity != null)
                {
                    closestCity.Population += cityMigrants;
                }
                victims += cityVictims;
                migrants += cityMigrants;
            }
        }
    }
}
