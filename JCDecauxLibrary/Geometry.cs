using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Geometry
    {
        public float[][] coordinates { get; set; }

        public Geometry() {}

        public override string ToString()
        {
            String output = "[";

            foreach (float[] coordinate in coordinates)
            {
                output += "[" + coordinate[0].ToString() + "," + coordinate[1].ToString() + "]";
            }

            output += "]";

            return output;
        }
    }
}
