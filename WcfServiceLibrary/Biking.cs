using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text.Json;
using JCDecauxLibrary;
using System.Globalization;

namespace WcfServiceLibrary
{
    public class Biking : IBiking
    {
        HttpClient client = new HttpClient();
        Proxy.ProxyServiceClient proxy = new Proxy.ProxyServiceClient();
        Utils utils = new Utils();
        Processing processing = new Processing();

        public Biking()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public string GetAddressCoordinates(string address)
        {
            try
            {
                string formattedAdress = address.Replace(" ", "%20");


                var response = client.GetStringAsync("https://api.openrouteservice.org/geocode/search?api_key=5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946&text=" + formattedAdress);
                response.Wait();

                Geocode geocode = JsonSerializer.Deserialize<Geocode>(response.Result);

                return JsonSerializer.Serialize(geocode);
            }
            catch
            {
                Console.WriteLine("Error while sending request (Searching for address coordinates).");
                return "Error while sending request (Searching for address coordinates).";
            }
        }

        public string GetNearestStations(double startLon, double startLat, double endLon, double endLat)
        {
            string stationsString = proxy.GetStationsOfContract("Lyon");
            
            Station[] stations = JsonSerializer.Deserialize<Station[]>(stationsString);

            Station[] answer = new Station[2];
            answer[0] = processing.getClosestStation(startLon, startLat, stations);
            answer[1] = processing.getClosestStation(endLon, endLat, stations);

            return JsonSerializer.Serialize(answer);
        }

        public string GetStartWalkDirections(double startLon, double startLat, double endLon, double endLat)
        {
            try
            {
                var response = client.GetStringAsync("https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946&start=" 
                    + startLon.ToString().Replace(',', '.')
                    + "," 
                    + startLat.ToString().Replace(',', '.')
                    + "&end=" 
                    + endLon.ToString().Replace(',', '.')
                    + "," 
                    + endLat.ToString().Replace(',', '.'));
                response.Wait();

                return response.Result;
            }
            catch
            {
                Console.WriteLine("Error while sending request (Searching for start walk directions).");
                return "Error while sending request (Searching for start walk directions).";
            }
        }

        public string GetEndWalkDirections(double startLon, double startLat, double endLon, double endLat)
        {
            try
            {
                var response = client.GetStringAsync("https://api.openrouteservice.org/v2/directions/foot-walking?api_key=5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946&start=" 
                    + startLon.ToString().Replace(',', '.')
                    + ","
                    + startLat.ToString().Replace(',', '.')
                    + "&end="
                    + endLon.ToString().Replace(',', '.')
                    + ","
                    + endLat.ToString().Replace(',', '.')); 
                response.Wait();

                return response.Result;
            }
            catch
            {
                Console.WriteLine("Error while sending request (Searching for end walk directions).");
                return "Error while sending request (Searching for end walk directions).";
            }
        }

        public string GetBikingDirections(double startLon, double startLat, double endLon, double endLat)
        {
            try
            {
                var response = client.GetStringAsync("https://api.openrouteservice.org/v2/directions/cycling-regular?api_key=5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946&start="
                    + startLon.ToString().Replace(',', '.')
                    + ","
                    + startLat.ToString().Replace(',', '.')
                    + "&end="
                    + endLon.ToString().Replace(',', '.')
                    + ","
                    + endLat.ToString().Replace(',', '.'));
                response.Wait();

                return response.Result;
            }
            catch
            {
                Console.WriteLine("Error while sending request (Searching for biking directions).");
                return "Error while sending request (Searching for biking directions).";
            }
        }

        public string getCoordinates(string startAddress, string endAddress)
        {
            string startingCoordinatesString = GetAddressCoordinates(startAddress);
            Geocode startingCoordinates = JsonSerializer.Deserialize<Geocode>(startingCoordinatesString);

            string endingCoordinatesString = GetAddressCoordinates(endAddress);
            Geocode endingCoordinates = JsonSerializer.Deserialize<Geocode>(endingCoordinatesString);

            string stationsString = GetNearestStations(startingCoordinates.features[0].geometry.coordinates[0], startingCoordinates.features[0].geometry.coordinates[1], endingCoordinates.features[0].geometry.coordinates[0], endingCoordinates.features[0].geometry.coordinates[1]);
            Station[] stations = JsonSerializer.Deserialize<Station[]>(stationsString);

            string startWalkingString = GetStartWalkDirections(startingCoordinates.features[0].geometry.coordinates[0], startingCoordinates.features[0].geometry.coordinates[1], stations[0].position.lng, stations[0].position.lat);
            OpenRoute startWalk = JsonSerializer.Deserialize<OpenRoute>(startWalkingString);

            string bikingString = GetStartWalkDirections(stations[0].position.lng, stations[0].position.lat, stations[1].position.lng, stations[1].position.lat);
            OpenRoute biking = JsonSerializer.Deserialize<OpenRoute>(bikingString);

            string endWalkingString = GetEndWalkDirections(stations[1].position.lng, stations[1].position.lat, endingCoordinates.features[0].geometry.coordinates[0], endingCoordinates.features[0].geometry.coordinates[1]);
            OpenRoute endWalk = JsonSerializer.Deserialize<OpenRoute>(endWalkingString);

            return JsonSerializer.Serialize(startWalk.features[0].geometry) + "@" + JsonSerializer.Serialize(biking.features[0].geometry) + "@" + JsonSerializer.Serialize(endWalk.features[0].geometry); 
        }
    }
}
