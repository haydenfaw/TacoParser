using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething() //Tests general functionality
        {
            //Arrange
            var tacoParser = new TacoParser();
            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");
            //Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        public void ShouldParseLongitude(string line, double expected) //Tests for success of parse on longitude
        {
            //Arrange
            var testLongitude = new TacoParser();
            //Act
            var actual = testLongitude.Parse(line);
            //Assert
            Assert.Equal(expected.ToString(), actual.Location.Longitude.ToString());
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        public void ShouldParseLatitude(string line, double expected) //Tests for success of parse on latitude
        {
            //Arrange
            var testLatitude = new TacoParser();
            //Act
            var actual = testLatitude.Parse(line);
            //Assert
            Assert.Equal(expected.ToString(), actual.Location.Latitude.ToString());
        }

    }
}