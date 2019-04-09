namespace DemographicSimulator.Simulator
{
    public class MapConfiguration
    {
        public MapConfiguration(double avgTemperature, double avgHeight)
        {
            AvgTemperature = avgTemperature;
            AvgHeight = avgHeight;
        }

        public double AvgTemperature { get; }
        public double AvgHeight { get; }

    }
}