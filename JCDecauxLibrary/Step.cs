using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public String instruction { get; set; }

        public override string ToString()
        {
            return "[" + instruction + " (distance : " + Math.Round(distance) + "m,  duration " + Math.Round(duration) + " sec)]";
        }
    }
}
