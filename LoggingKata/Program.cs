using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        //const string csvPath = "TacoBell-US-AL.csv";
        //Testing Functionality
        //const string csvPath = "TestingLocations.csv";
        //const string csvPath = "TestingLocationsFailures.csv";
        const string csvPath = "TestingLocationsFailures2.csv";
        //const string csvPath = "TestingLocationsFailures3.csv";
        //const string csvPath = "TestingLocationsFailures4.csv";
        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            //Uses File.ReadAllLines(path) to grab all the lines from your csv file            
            var lines = File.ReadAllLines(csvPath);
            var didnotbreak = 0;
            try
            {
                if (lines.Count() < 3)
                {
                    if (lines.Count() == 0)
                    {
                        logger.LogFatal($"There were 0 lines grabbed. The program will not work correctly and will now terminate.");
                        didnotbreak = 1;
                    }
                    if (lines.Count() == 1)
                    {
                        logger.LogWarning($"There was only 1 line grabbed. The program will not work correctly and will now terminate.");
                        didnotbreak = 1;
                    }
                }
            }
            catch (Exception e)
            {
            }
            if (didnotbreak == 0 && lines.Count() >= 2)
            { 
                //wasDONE? -- Create a new instance of your TacoParser class
                var parser = new TacoParser();

                //wasDONE? -- Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
                var locations = lines.Select(parser.Parse).ToArray();
                locations = locations.Where(x => x != null).ToArray();
                // DON'T FORGET TO LOG YOUR STEPS

            //If less than 2 locations exist in csv to calculate distance between, logs fatal error and terminates program
            if (locations.Where(x => x != null).Count() < 2) 
            {
                logger.LogFatal("There were less than 2 locations found. Terminating program");
                return;
            }

            //Create Itrackable variables to store two taco bell locations that are the farthest from each other, and a double distance for their distance
            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0;
            ITrackable finalLocA = null;
            ITrackable finalLocB = null;

            //Double loop to find the two farthest locations from each other
            for (int i = 0; i < locations.Length; i++)
            {
                locA = locations[i];
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                for (int j = 0; j < locations.Length; j++)
                {
                    locB = locations[j];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    double possibleNewDistance = corA.GetDistanceTo(corB);

                        if (possibleNewDistance > distance)
                        {
                            distance = possibleNewDistance;
                            finalLocA = locations[i];
                            finalLocB = locations[j];
                        }
                    }
                }
                if (finalLocA != null && finalLocB != null)
                {
                    Console.WriteLine($"=================================================================");
                    Console.WriteLine($"The farthest two tacobells are: {finalLocA.Name} and {finalLocB.Name} with the distance of {distance}");
                }
                if (finalLocA == null || finalLocB == null)
                {
                    Console.WriteLine($"=================================================================");
                    Console.WriteLine($"Sorry but there was not enough information read to find the distance between any two TacoBells.");
                }
            }
            //DONE -- HINT NESTED LOOPS SECTION---------------------
            //DONE --  Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            //DONE --  Create a new corA Coordinate with your locA's lat and long

            //DONE --  Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            //DONE --  Create a new Coordinate with your locB's lat and long

            //DONE --  Now, compare the two using `.GetDistanceTo()`, which returns a double
            //DONE --  If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            //DONE --  Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

        }
    }
}
