using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Coordinate
    {
        public string[] values;

        public override string ToString()
        {
            return "[" + values[0] + ", " + values[1] + "]";
        }
    }
}
