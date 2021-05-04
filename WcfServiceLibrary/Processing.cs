using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCDecauxLibrary;

namespace WcfServiceLibrary
{
    class Processing
    {
        public double getDistanceFrom2GpsCoordinates(double lat1, double lon1, float lat2, float lon2)
        {
            // Radius of the earth in km
            int earthRadius = 6371;
            double dLat = deg2rad(lat2 - lat1);
            double dLon = deg2rad(lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
                ;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = earthRadius * c; // Distance in km
            return d;
        }

        public double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public Station getClosestStationEnd(double lon, double lat, Station[] stations) 
        {
            Station nearest = stations[0];
            double bestDistance = getDistanceFrom2GpsCoordinates(lat, lon, stations[0].position.lat, stations[0].position.lng);

            foreach (Station station in stations)
            {
                if (station.available_bike_stands > 0)
                {
                    var currentDistance = getDistanceFrom2GpsCoordinates(lat, lon, station.position.lat, station.position.lng);
                    if (currentDistance < bestDistance)
                    {
                        bestDistance = currentDistance;
                        nearest = station;
                    }
                }
            }

            return nearest;
        }

        public Station getClosestStationStart(double lon, double lat, Station[] stations)
        {
            Station nearest = stations[0];
            double bestDistance = getDistanceFrom2GpsCoordinates(lat, lon, stations[0].position.lat, stations[0].position.lng);

            foreach (Station station in stations)
            {
                if (station.available_bikes > 0)
                {
                    var currentDistance = getDistanceFrom2GpsCoordinates(lat, lon, station.position.lat, station.position.lng);
                    if (currentDistance < bestDistance)
                    {
                        bestDistance = currentDistance;
                        nearest = station;
                    }
                }
            }

            return nearest;
        }
    }
}
