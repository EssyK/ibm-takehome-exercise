using MarsRover;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MarsRoverTest
{
    [TestClass]
    public class RoverNavigationTests
    {
        [TestMethod]
        public void ShouldReturnFalseForInvalidInput()
        {
            RoverNavigation roverNavigation = new RoverNavigation(new string[] { });
            Assert.IsFalse(roverNavigation.IsValid);
        }

        [TestMethod]
        public void ShouldGetRoverCoordinates()
        {
            //given
            RoverNavigation roverNavigation = new RoverNavigation();
            string upperRightCoordinates = "5 5";
            string roverPosition = "3 3 E";
            string instruction = "MMRMMRMRRM";
            string expected = "5 1 E";

            //when
            var result = roverNavigation.GetFinalCoordinates(upperRightCoordinates, roverPosition, instruction);

            //then
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldGetCoordinatesForMultipleRovers()
        {
            //given
            RoverNavigation roverNavigation = new RoverNavigation(GetValidInput());
            var expected = new List<string>() { "1 3 N" , "5 1 E" };
            int i = 0;

            //when
            var result = roverNavigation.Navigate();
            
            //then
            foreach (var actual in result)
            {
                Assert.AreEqual(expected[i++], actual);
            }
        }

        private string[] GetValidInput()
        {
            return new string[] {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
            };
        }

    }
}
