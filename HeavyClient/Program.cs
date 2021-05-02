using System;
using JCDecauxLibrary;

namespace HeavyClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Biking.BikingClient bikingClient = new Biking.BikingClient();
            Utils utils = new Utils();

            while (true)
            {
                Console.WriteLine("Entrez une addresse de départ");
                string startingAddress = Console.ReadLine();

                Console.WriteLine("Entrez une addresse d'arrivée");
                string endingAddress = Console.ReadLine();

                string startingCoordinatesString = bikingClient.GetAddressCoordinates(startingAddress);
                Geocode startingCoordinates = System.Text.Json.JsonSerializer.Deserialize<Geocode>(startingCoordinatesString);

                string endingCoordinatesString = bikingClient.GetAddressCoordinates(endingAddress);
                Geocode endingCoordinates = System.Text.Json.JsonSerializer.Deserialize<Geocode>(endingCoordinatesString);

                string stationsString = bikingClient.GetNearestStations(startingCoordinates.features[0].geometry.coordinates[0], startingCoordinates.features[0].geometry.coordinates[1], endingCoordinates.features[0].geometry.coordinates[0], endingCoordinates.features[0].geometry.coordinates[1]);
                Station[] stations = System.Text.Json.JsonSerializer.Deserialize<Station[]>(stationsString);
                utils.displayStations(stations);

                Console.WriteLine("Walking :");

                string answerStartWalk = bikingClient.GetStartWalkDirections(4.791202, 45.786766, stations[0].position.lng, stations[0].position.lat);
                OpenRoute startWalk = System.Text.Json.JsonSerializer.Deserialize<OpenRoute>(answerStartWalk);

                foreach (Step step in startWalk.features[0].properties.segments[0].steps)
                {
                    Console.WriteLine(step);
                }

                Console.WriteLine("Biking :");

                string answerBiking = bikingClient.GetBikingDirections(stations[0].position.lng, stations[0].position.lat, stations[1].position.lng, stations[1].position.lat);
                OpenRoute bikingRoute = System.Text.Json.JsonSerializer.Deserialize<OpenRoute>(answerBiking);

                foreach (Step step in bikingRoute.features[0].properties.segments[0].steps)
                {
                    Console.WriteLine(step);
                }

                Console.WriteLine("Walking :");

                string answerEndWalk = bikingClient.GetEndWalkDirections(stations[1].position.lng, stations[1].position.lat, 4.844489, 45.775232);
                OpenRoute endWalk = System.Text.Json.JsonSerializer.Deserialize<OpenRoute>(answerEndWalk);

                foreach (Step step in endWalk.features[0].properties.segments[0].steps)
                {
                    Console.WriteLine(step);
                }

                Console.WriteLine("Durée totale du trajet : " + Math.Round((startWalk.features[0].properties.segments[0].duration + bikingRoute.features[0].properties.segments[0].duration + endWalk.features[0].properties.segments[0].duration)/60) + " min");
                Console.WriteLine("Distance totale du trajet : " + Math.Round((startWalk.features[0].properties.segments[0].distance + bikingRoute.features[0].properties.segments[0].distance + endWalk.features[0].properties.segments[0].distance)) + " m");
                Console.WriteLine();
            }
        }
    }
}