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
            IsChanged = false;
        }
        public bool IsChanged { get; set; }
        public double AvgTemperature { get; set; }
        public double AvgHeight { get; set; }
        public int DevelopmentLevel { get; set; }
        public double Birthrate { get; set; }
    }

}