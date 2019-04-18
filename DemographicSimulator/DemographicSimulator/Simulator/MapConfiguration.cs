namespace DemographicSimulator.Simulator
{
    public class MapConfiguration
    {
        public MapConfiguration(double avgTemperature, double avgHeight, int developmentLevel, double birthrate)
        {
            AvgTemperature = avgTemperature;
            AvgHeight = avgHeight;
            DevelopmentLevel = developmentLevel;
            Birthrate = birthrate;
        }

        public double AvgTemperature { get; }
        public double AvgHeight { get; }
        public int DevelopmentLevel { get; }
        public double Birthrate { get; }
    }

}