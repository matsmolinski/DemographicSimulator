using System;
using System.Collections.Generic;
using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;

namespace DemographicSimulator.Events
{
    class Earthquake : IGameEvent
    {

        public void ProceedEvent(Map map, Point center, int power, out int victims, out int migrants)
        {
            List<City> safeCities = new List<City>();
            List<City> endangeredCities = new List<City>();

            foreach(City c in map.Cities)
            {
                if (c.Distance(center) >= 100)
                {
                    safeCities.Add(c);
                }
                else
                {
                    endangeredCities.Add(c);
                }
            }
            victims = migrants = 0;
            foreach(City c in endangeredCities)
            {
                Random r = new Random();
                double factor = ((100 - c.Distance(center)) / 100) * power / 100;
                Console.WriteLine(factor);
                int cityVictims = (int)(10000 * factor * (1.6 + r.NextDouble()) / 2.6);
                int cityMigrants = (int)(50000 * factor * (1+r.NextDouble())/2);
                if (c.Population < 100000)
                {
                    if(factor < 0.7)
                    {
                        cityMigrants = (int)(c.Population * factor);
                        c.Population -= cityMigrants;
                        cityVictims = (int)(c.Population * factor * r.NextDouble());
                        c.Population -= cityVictims;
                    }
                    else
                    {
                        cityMigrants = (int)(c.Population * factor * 0.5);
                        c.Population -= cityMigrants;
                        cityVictims = (int)(c.Population * factor * r.NextDouble());
                        c.Population -= cityVictims;
                    }
                }
                else
                {
                    c.Population -= cityVictims;
                    c.Population -= cityMigrants;
                }
                double minDistance = double.MaxValue;
                City closestCity = null;
                foreach(City sc in safeCities)
                {
                    if(c.Distance(sc.point) < minDistance)
                    {
                        minDistance = c.Distance(sc.point);
                        closestCity = sc;
                    }
                }
                if(closestCity != null)
                {
                    closestCity.Population += cityMigrants;
                }
                victims += cityVictims;
                migrants += cityMigrants;
            }
        }
        public override string ToString()
        {
            return "Earthquake!";
        }
    }
}
