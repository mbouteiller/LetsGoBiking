using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class OpenRoute
    {
        public Feature[] features { get; set; }

        public override string ToString()
        {
            return features[0].ToString();
        }
    }
}