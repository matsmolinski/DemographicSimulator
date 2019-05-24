using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;

namespace DemographicSimulator.Events
{
    public interface IGameEvent
    {
        void ProceedEvent(Map map, Point center, int power, out int victims, out int migrants);
        string ToString();
    }
}
