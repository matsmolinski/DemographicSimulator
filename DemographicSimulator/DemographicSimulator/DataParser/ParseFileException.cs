using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.DataParser
{
    class ParseFileException : Exception
    {
        public ParseFileException(string s) : base(s) { }
    }
}
