using System;

namespace DemographicSimulator.DataParser
{
    class ParseFileException : Exception
    {
        public ParseFileException(string s) : base(s) { }
    }
}
