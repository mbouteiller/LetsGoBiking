using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Property
    {
        public Segment[] segments { get; set; }

        public override string ToString()
        {
            return segments[0].ToString();
        }
    }
}
