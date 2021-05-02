using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Segment
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public Step[] steps { get; set; }

        public override string ToString()
        {
            String output = "";

            output += "[" + distance + ", " + duration;

            foreach (Step step in steps)
            {
                output += step.ToString();
            }

            output += "]";

            return output ;
        }
    }
}
