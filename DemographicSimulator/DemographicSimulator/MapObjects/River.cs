using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemographicSimulator.MapObjects
{
    public class River
    {
        public readonly Line[] riverSegments;

        public River(Line[] riverSegments)
        {
            this.riverSegments = riverSegments;
        }

        public double Distance(Point point)
        {
            //math
            return 0;
        }
    }
}
