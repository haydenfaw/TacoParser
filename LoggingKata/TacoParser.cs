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

        public ITrackable Parse(string line) //Converts string into an ITrackable (TacoBell)
        {
            logger.LogInfo("Begin parsing");

            //Takes line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            //If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                logger.LogError($"Had less than 3 cells: {line}");
                return null;
            }

            //Generate variables for latitude, longitude, and the store name
            var latitude = double.Parse(cells[0]);
            var longitude = double.Parse(cells[1]);
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