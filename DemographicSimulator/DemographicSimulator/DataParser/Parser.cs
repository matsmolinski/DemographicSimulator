using DemographicSimulator.MapObjects;
using DemographicSimulator.Simulator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.DataParser
{
    public class Parser
    {
        private class ParseLineException : Exception
        {
            public ParseLineException(string s) : base(s) { }
        }
        private enum ParserState
        {
            START, CONTOURPOINTS, RIVERS, CITIES, GLOBAL
        }
        public static Map ReadData(string dataPath, out List <string> feedback)
        {
            ParserState state = ParserState.START;
            int lineCounter = 0, currentRiver = 0;
            List<Point> currentRiverSegments = new List<Point>();
            Map map = new Map();
            MapConfiguration mc = new MapConfiguration(8, 10, 5, 5);
            StreamReader reader = new StreamReader(dataPath);
            string line;
            feedback = new List<string>();
            List<Point> contourPoints = new List<Point>();
            while ((line = reader.ReadLine()) != null)
            {
                lineCounter++;
                switch (state)
                {
                    case ParserState.START:
                        if (line.Length > 0 && line[0] == '#')
                        {
                            state = ParserState.CONTOURPOINTS;
                        }
                        else
                        {
                            throw new ParseFileException("File does not start with proper commentary line");
                        }
                        break;
                    case ParserState.CONTOURPOINTS:
                        if (line.Length > 0 && line[0] == '#')
                        {
                            for(int i = 0; i < contourPoints.Count - 1; i++)
                            {
                                map.ContourLines.Add(new Line(contourPoints[i], contourPoints[i + 1]));
                            }
                            map.ContourLines.Add(new Line(contourPoints[contourPoints.Count - 1], contourPoints[0]));
                            state = ParserState.RIVERS;
                        }
                        else
                        {
                            try
                            {
                                contourPoints.Add(ParseContourPoint(line));
                            }
                            catch (ParseLineException e)
                            {
                                feedback.Add(e.Message + " in line " + lineCounter);
                            }
                        }
                        break;
                    case ParserState.RIVERS:
                        if (line.Length > 0 && line[0] == '#')
                        {
                            state = ParserState.CITIES;
                        }
                        else
                        {
                            try
                            {
                                ParseRiverSegment(line, ref currentRiver, ref currentRiverSegments, map);                               
                            }
                            catch (ParseLineException e)
                            {
                                feedback.Add(e.Message + " in line " + lineCounter);
                            }
                        }
                        break;
                    case ParserState.CITIES:
                        if (line.Length > 0 && line[0] == '#')
                        {
                            state = ParserState.GLOBAL;
                        }
                        else
                        {
                            try
                            {
                                map.Cities.Add(ParseCity(line));
                            }
                            catch (ParseLineException e)
                            {
                                feedback.Add(e.Message + " in line " + lineCounter);
                            }
                        }
                        break;
                    case ParserState.GLOBAL:
                        if (line.Length > 0 && line[0] == '#')
                        {
                            throw new ParseFileException("File contains more than four comment lines. There should be four lines starting with a hash symbol. Please verify the file.");
                        }
                        else
                        {
                            try
                            {
                                ParseGlobalData(line, ref mc);
                            }
                            catch (ParseLineException e)
                            {
                                feedback.Add(e.Message + " in line " + lineCounter);
                            }
                        }
                        break;
                }
            }
            map.mc = mc;
            return map;
        }

        private static void ParseGlobalData(string line, ref MapConfiguration mc)
        {
            string[] elements = line.Split();
            if (elements.Length > 2)
            {
                throw new ParseLineException("Too many arguments");
            }
            if (!double.TryParse(elements[1], out double va))
            {
                throw new ParseLineException("Height has to be a floating point number");
            }
            switch (elements[0])
            {
                case "przyrost_naturalny":
                    mc.Birthrate = va;
                    break;

                case "stopien_rozwoju":
                    mc.DevelopmentLevel = va;
                    break;

                case "srednia_temperatura":
                    mc.AvgTemperature = va;
                    break;

                case "srednia_wysokosc":
                    mc.AvgHeight = va;
                    break;
            }

        }

        private static City ParseCity(string line)
        {
            string[] elements = line.Split();
            if (elements.Length > 5)
            {
                throw new ParseLineException("Too many arguments");
            }
            try
            {
                if (!int.TryParse(elements[0], out int x))
                {
                    throw new ParseLineException("X position has to be an integer");
                }
                if (!int.TryParse(elements[1], out int y))
                {
                    throw new ParseLineException("Y position has to be an integer");
                }
                if (!int.TryParse(elements[2], out int pop))
                {
                    throw new ParseLineException("Population has to be an integer");
                }
                if (x < 0 || x > 600)
                {
                    throw new ParseLineException("X coordinate out of range");
                }
                if (y < 0 || y > 600)
                {
                    throw new ParseLineException("Y coordinate out of range");
                }
                City c = new City(new Point(x, y), pop, elements[3]);
                if (elements.Length > 4)
                {
                    if (!double.TryParse(elements[2], out double height))
                    {
                        throw new ParseLineException("Height has to be a floating point number");
                    }
                    c.CityData.Height = height;
                }
                return c;
            }
            catch (IndexOutOfRangeException)
            {
                throw new ParseLineException("Too few arguments");
            }
        }
    

        private static void ParseRiverSegment(string line, ref int currentRiver, ref List<Point> currentRiverSegments, Map map)
        {
            string[] elements = line.Split();
            if(elements[0] != currentRiver.ToString())
            {
                Line[] currentRiverLines = new Line [currentRiverSegments.Count];
                for (int i = 0; i < currentRiverSegments.Count - 1; i++)
                {
                    currentRiverLines[i] = new Line(currentRiverSegments[i], currentRiverSegments[i + 1]);
                }
                currentRiverLines[currentRiverSegments.Count - 1] = 
                    new Line(currentRiverSegments[currentRiverSegments.Count - 1], currentRiverSegments[0]);
                map.Rivers.Add(new River(currentRiverLines));
                if (!int.TryParse(elements[0], out int no))
                {
                    throw new ParseLineException("River index is not proper integer");
                }
                currentRiver = no;
                currentRiverSegments.Clear();
            }
            if (!int.TryParse(elements[1], out int x))
            {
                throw new ParseLineException("X position has to be an integer");
            }
            if (!int.TryParse(elements[2], out int y))
            {
                throw new ParseLineException("Y position has to be an integer");
            }
            if (x < 0 || x > 600)
            {
                throw new ParseLineException("X coordinate out of range");
            }
            if (y < 0 || y > 600)
            {
                throw new ParseLineException("Y coordinate out of range");
            }
            currentRiverSegments.Add(new Point(x, y));
        }

        public static Point ParseContourPoint(string line)
        {
            string[] elements = line.Split();
            if (elements.Length > 2)
            {
                throw new ParseLineException("Too many arguments");
            }
            try
            {
                if (!int.TryParse(elements[0], out int x))
                {
                    throw new ParseLineException("X position has to be an integer");
                }
                if (!int.TryParse(elements[1], out int y))
                {
                    throw new ParseLineException("Y position has to be an integer");
                }
                if (x < 0 || x > 600)
                {
                    throw new ParseLineException("X coordinate out of range");
                }
                if (y < 0 || y > 600)
                {
                    throw new ParseLineException("Y coordinate out of range");
                }
                return new Point(x, y);
            }
            catch (IndexOutOfRangeException)
            {
                throw new ParseLineException("Too few arguments");
            }
        }

    }

   
}
