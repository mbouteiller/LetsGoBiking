using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Feature
    {
        public Property properties { get; set; }
        public Geometry geometry { get; set; }

        public override string ToString()
        {
            return properties.ToString() + geometry.ToString();
        }

    }
}
