using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Position
    {
        public float lat { get; set; }
        public float lng { get; set; }

        public override string ToString()
        {
            return "[" + lng + ", " + lat + "]";
        }
    }
}
