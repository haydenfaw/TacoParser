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
            //var didnotbreak = 0;

            //Log and error if you get 0 lines and a warning if you get 1 line
            if (lines.Count() < 3)
            {
                if (lines.Count() == 0)
                {
                    logger.LogFatal($"There were 0 lines grabbed. The program will not work correctly and will now terminate.");
                    //didnotbreak = 1;
                }
                if (lines.Count() == 1)
                {
                    logger.LogWarning($"There was only 1 line grabbed. The program will not work correctly and will now terminate.");
                    //didnotbreak = 1;
                }
            }

            //if (didnotbreak == 0 && lines.Count() >= 2)
            //{ 
                //Creates a new instance of your TacoParser class
                var parser = new TacoParser();

                //Grabs an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
                var locations = lines.Select(parser.Parse).ToArray();
                locations = locations.Where(x => x != null).ToArray();

                // DON'T FORGET TO LOG YOUR STEPS

                //Create two Itrackable variables to store two taco bell locations that are the farthest from each other, and a double distance for their distance
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
                            //locA = locations[i];
                            //locB = locations[j];
                            finalLocA = locations[i];
                            finalLocB = locations[j];                            
                        }
                    }
                }
                //if (locA != null && locB != null)
                if (finalLocA != null && finalLocB != null)
                {
                    Console.WriteLine($"=================================================================");
                    Console.WriteLine($"The farthest two tacobells are: {finalLocA.Name} and {finalLocB.Name} with the distance of {distance}");
                    //Console.WriteLine($"The farthest two tacobells are: {locA.Name} and {locB.Name} with the distance of {distance}");
                }
                //if (locA == null && locB == null)
                if (finalLocA == null || finalLocB == null)                
                {
                    Console.WriteLine($"=================================================================");
                    Console.WriteLine($"Sorry but there was not enough information read to find the distance between any two TacoBells.");
                }
            //}
        }
    }
}
