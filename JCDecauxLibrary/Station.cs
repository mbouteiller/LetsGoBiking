using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Station
    {
        public int number { get; set; }
        public string contract_name { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public Position position { get; set; }
        public int available_bike_stands { get; set; }


        public override string ToString()
        {
            string description = "";

            description += number.ToString() + " "
                + contract_name + " "
                + name + " "
                + address + " "
                + status + " "
                + position.ToString()
                + " Stands available : " + available_bike_stands;

            return description;
        }
    }
}
