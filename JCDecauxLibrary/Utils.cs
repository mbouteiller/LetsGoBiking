using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCDecauxLibrary
{
    public class Utils
    {
        public void displayStations(Station[] stations)
        {
            foreach (Station station in stations)
            {
                Console.WriteLine(station.ToString());
            }
        }
        public void displayStation(Station station)
        {
            Console.WriteLine(station.ToString());
        }
    }
}
