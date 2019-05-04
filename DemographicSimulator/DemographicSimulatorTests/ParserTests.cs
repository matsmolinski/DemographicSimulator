using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemographicSimulator.DataParser;
using System.Collections.Generic;
using DemographicSimulator.Simulator;
using DemographicSimulator.MapObjects;

namespace DemographicSimulatorTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestContour()
        {
            Map map = Parser.ReadData("../../data1.txt", out List<string> fb);
            /*string list = "";
            for(int i = 0; i < fb.Count; i++)
            {
                list += (fb[i] + "\n");
            }*/
            //throw new Exception(list);
            //throw new Exception(map.ContourLines[0].ToString());
            Assert.AreEqual(5, map.ContourLines.Count);
            Assert.AreEqual(new Line(0, 0, 0, 20), map.ContourLines[0]);
            Assert.AreEqual(new Line(40, 0, 0, 0), map.ContourLines[4]);
        }

        [TestMethod]
        public void TestRivers()
        {
            Map map = Parser.ReadData("../../data1.txt", out List<string> fb);
            Assert.AreEqual(2, map.Rivers.Count);
            Assert.AreEqual(2, map.Rivers[0].riverSegments.Length);
            Assert.AreEqual(1, map.Rivers[1].riverSegments.Length);
            Assert.AreEqual(new Line(10, 10, 10, 50), map.Rivers[0].riverSegments[0]);
            Assert.AreEqual(new Line(10, 50, 60, 50), map.Rivers[0].riverSegments[1]);
            Assert.AreEqual(new Line(43, 23, 43, 10), map.Rivers[1].riverSegments[0]);
        }

        [TestMethod]
        public void TestCities()
        {
            Map map = Parser.ReadData("../../data1.txt", out List<string> fb);
            Assert.AreEqual(3, map.Cities.Count);
            Assert.AreEqual(1700000, map.Cities[0].Population);
            Assert.AreEqual(5, map.Cities[1].CityData.Height);
            Assert.AreEqual("Wroclaw", map.Cities[2].name);
            Assert.AreEqual(new Point(30, 10), map.Cities[2].point);
        }

        [TestMethod]
        public void TestGlobals()
        {
            Map map = Parser.ReadData("../../data1.txt", out List<string> fb);
            Assert.AreEqual(10, map.mc.AvgHeight);
            Assert.AreEqual(8, map.mc.AvgTemperature);
            Assert.AreEqual(4, map.mc.Birthrate);
            Assert.AreEqual(2, map.mc.DevelopmentLevel);
        }
    }
}
