using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class GeoGeometry
    {
        public List<double> coordinates { get; set; }
    }

    public class GeoFeature
    {
        public GeoGeometry geometry { get; set; }
    }

    public class Geocode
    {
        public List<GeoFeature> features { get; set; }
    }
}
