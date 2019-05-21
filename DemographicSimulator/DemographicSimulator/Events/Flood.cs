using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;

namespace DemographicSimulator.Events
{
    public class Flood : IGameEvent
    {

        public void ProceedEvent(Map map, Point center, int power, out int victims, out int migrants)
        {
            List<City> safeCities = new List<City>();
            List<City> endangeredCities = new List<City>();

            foreach (City c in map.Cities)
            {
                if (c.Distance(center) >= 20)
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
                double factor = ((20 - c.Distance(center)) / 20) * power / 100;
                Console.WriteLine(factor);
                int cityVictims = (int)(10 * factor * (0.6 + r.NextDouble()) / 1.6);
                int cityMigrants = (int)(10000 * factor * (0.7 + r.NextDouble()) / 1.7);
                if (c.Population < 10000)
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
