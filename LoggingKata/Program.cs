using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";
        //Testing Functionality
        //const string csvPath = "TestingLocations.csv";
        //const string csvPath = "TestingLocationsFailures.csv";
        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            //DONE -- use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            if(lines.Count() == 0)
            {
                //Log here ? (not sure what that means)
                logger.LogError("There were 0 lines grabbed");
            }
            if(lines.Count() == 1)
            {
                logger.LogWarning("There was only 1 line grabbed.");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            //wasDONE? -- Create a new instance of your TacoParser class
            var parser = new TacoParser();

            //wasDONE? -- Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            //DONE -- TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            //DONE -- Create a `double` variable to store the distance
            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0;
            ITrackable FinalLocA = null;
            ITrackable FinalLocB = null;
            //wasDONE? -- Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            for (int i=0; i<lines.Length; i++)
            {
                locA = locations[i];
                //double[] corA = {locA.Location.Latitude, locA.Location.Longitude };
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                
                for(int j=0; j<lines.Length; j++)
                {
                    locB = locations[j];
                    //double[] corB = {locB.Location.Latitude, locB.Location.Longitude};
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    double possibleNewDistance = corA.GetDistanceTo(corB);
                    //var distanceBetween = corA.GetDistanceTo(corB); //GetDistanceTo(corA, corB);

                    if (possibleNewDistance > distance)
                    {
                        distance = possibleNewDistance;
                        FinalLocA = locations[i];
                        FinalLocB = locations[j];
                    }
                }
            }
            Console.WriteLine($"The furthest two tacobells are: {FinalLocA.Name} and {FinalLocB.Name} with the distance of {distance}");
            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
                        
        }
    }
}
