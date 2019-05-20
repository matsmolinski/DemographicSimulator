using DemographicSimulator.MapObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DemographicSimulatorTests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void TestRiverDistance1()
        {
            Line[] segments = { new Line(0, 0, 100, 0), new Line(100, 0, 100, 20) };
            River r = new River(segments);
            double dist = r.Distance(new Point(30, 10));
            Assert.AreEqual(10, dist, 1e-5);
            dist = r.Distance(new Point(90, 15));
            Assert.AreEqual(10, dist, 1e-5);
            dist = r.Distance(new Point(100, 200));
            Assert.AreEqual(180, dist, 1e-5);
            dist = r.Distance(new Point(90, 50));
            Assert.AreEqual(Math.Sqrt(1000), dist, 1e-5);
        }
    }
}
