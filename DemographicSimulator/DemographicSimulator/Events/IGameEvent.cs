using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.Events
{
    public interface IGameEvent
    {
        Map ProceedEvent(Map map, Point center);
    }
}
