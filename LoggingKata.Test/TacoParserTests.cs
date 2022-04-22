using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething() //Did nothing but it works?
        {
            // TODO: Complete Something, if anything

            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //DONE -- TODO: Complete - "line" represents input data we will Parse to
            //DONE --       extract the Longitude.  Your .csv file will have many of these lines,
            //DONE --       each representing a TacoBell location

            //Arrange
            var testLongitude = new TacoParser();
            //Act
            var actual = testLongitude.Parse(line);
            //Assert
            Assert.Equal(expected.ToString(), actual.Location.Longitude.ToString());
        }

        //DONE -- TODO: Create a test ShouldParseLatitude
        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var testLatitude = new TacoParser();
            //Act
            var actual = testLatitude.Parse(line);
            //Assert
            Assert.Equal(expected.ToString(), actual.Location.Latitude.ToString());
        }
        //[Theory]
        //[InlineData("34.073638, -84.677017, Taco Bell Acwort...", !null)]
        //[InlineData("34.073638, Taco Bell Acwort...", null)]
        //public void CanFinishParse(string line)
        //{
        //    //Arrange
        //    var testProgramLogFailures = new TacoParser();
        //    //Act
        //    ITrackable actual;
        //    try
        //    {
        //        actual = testProgramLogFailures.Parse(line);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"Unable to parse");
        //    }
        //    //Assert
        //    Assert.actual.ToString();
        //}
    }
}
