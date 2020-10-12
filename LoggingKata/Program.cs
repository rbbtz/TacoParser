﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Reflection.Emit;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");
            logger.LogWarning($"Warning: {lines[1]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS
            logger.LogInfo("Read all lines, stored IEnum of Point.Locations in locations using parse method");
            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double savedDistance = 0;
            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`
            for (int i = 0; i < locations.Length; i++)
            {
                //HINT NESTED LOOPS SECTION---------------------
                // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
                var locA = locations[i];
                // Create a new corA Coordinate with your locA's lat and long
                var corA = locA.Location;
                var originGeo = new GeoCoordinate();
                originGeo.Latitude = corA.Latitude;
                originGeo.Longitude = corA.Longitude;

                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int i2 = 1; i2 < locations.Length ; i2++)
                {
                    var locB = locations[i2];
                    var corB = locB.Location;
                    var destinationGeo = new GeoCoordinate();
                    destinationGeo.Latitude = corB.Latitude;
                    destinationGeo.Longitude = corB.Longitude;

                    var totalDistance = originGeo.GetDistanceTo(destinationGeo);
                    if (totalDistance > savedDistance)
                    {
                        savedDistance = totalDistance;

                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
                // Create a new Coordinate with your locB's lat and long

                // Now, compare the two using `.GetDistanceTo()`, which returns a double
                // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

                // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            }
            Console.WriteLine();
            Console.WriteLine("Two furthest TacoBells are:");
            Console.WriteLine($"Name: {tacoBell1.Name}, Lat: {tacoBell1.Location.Latitude}, Long: {tacoBell1.Location.Longitude}");
            Console.WriteLine($"Name: {tacoBell2.Name}, Lat: {tacoBell2.Location.Latitude}, Long: {tacoBell2.Location.Longitude}");

            Console.ReadLine();
        }
    }
}
