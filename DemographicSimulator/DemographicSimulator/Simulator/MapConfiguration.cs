namespace DemographicSimulator.Simulator
{
    public class MapConfiguration
    {
        public MapConfiguration(double avgTemperature, double avgHeight, double developmentLevel, double birthrate)
        {
            AvgTemperature = avgTemperature;
            AvgHeight = avgHeight;
            DevelopmentLevel = developmentLevel;
            Birthrate = birthrate;
        }

        public double AvgTemperature { get; set; }
        public double AvgHeight { get; set; }
        public double DevelopmentLevel { get; set; }
        public double Birthrate { get; set; }
    }

}