using System;
using System.Collections.Generic;
using System.Linq;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line) //converts string into an ITrackable (TacoBell) - HF
        {
            logger.LogInfo("Begin parsing");

            //Takes line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            //If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                logger.LogError($"Had less than 3 cells: {line}");
                //Log that and return null
                //Do not fail if one record parsing fails, return null
                return null;
            }

            //Grabs the latitude from array at index 0
            var latitude = double.Parse(cells[0]);
            //Grabs the longitude from array at index 1
            var longitude = double.Parse(cells[1]);
            //Grabs the name from array at index 2
            var storeName = cells[2];

            //Creates new instance of the TacoBell Class
            var tacoBell = new TacoBell();
            //Stores name and location for said instance of the TacoBell Class
            tacoBell.Name = storeName;
            tacoBell.Location = new Point { Longitude = longitude, Latitude = latitude };

            return tacoBell;
        }
    }
}