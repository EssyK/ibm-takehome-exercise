using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class RoverNavigation
    {
        readonly string[] NASAInput;
        public readonly bool IsValid;

        public RoverNavigation()
        {
            IsValid = false;
        }

        public RoverNavigation(string[] input)
        {
            NASAInput = input;
            IsValid = ValidateInput(input);
        }

        private bool ValidateInput(string[] input)
        {
            if(input.Length == 0) return false;
            return true;
        }

        public List<string> Navigate()
        {
            var upperRightCoordinates = NASAInput[0];
            List<string> output = new List<string>();
            int n = NASAInput.Length;

            for (int i = 1; i < n; i++)
            {
                var roverPosition = NASAInput[i];
                var instruction = NASAInput[i+1];
                i++;

                var coordinates = GetFinalCoordinates(upperRightCoordinates, roverPosition, instruction);
                Console.WriteLine(coordinates);
                output.Add(coordinates);
            }
            return output;
        }

        /// <summary>
        /// This function calculates the final coordinates of a rover
        /// </summary>
        /// <param name="upperRightCoordinates"></param>
        /// <param name="roverPosition"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public string GetFinalCoordinates(string upperRightCoordinates, string roverPosition, string instruction)
        {
            var compassDict = GetCompass();
            var movementDict = GetDirectionMovement();

            //The upper Right coordinates provide the boundary beyond which the rover cannot go
            var urcSplit = upperRightCoordinates.Split(' ');
            Coordinate boundary = new Coordinate(Convert.ToInt32(urcSplit[0]), Convert.ToInt32(urcSplit[1]));

            var positionSplit = roverPosition.Split(' ');
            Coordinate coordinate = new Coordinate(Convert.ToInt32(positionSplit[0]), Convert.ToInt32(positionSplit[1]));
            RoverPosition roverPosition2 = new RoverPosition(coordinate, positionSplit[2].ToString());

            var currentX = coordinate.x;
            var currentY = coordinate.y;
            var currentPosition = roverPosition2.Position;

            for (int i = 0; i < instruction.Length; i++)
            {
                var dir = instruction[i].ToString();

                if (dir == "M")
                {
                    var move = movementDict[currentPosition];

                    if (move.Contains("+"))
                    {
                        if (move.Contains("y") && currentY + 1 <= boundary.y) currentY++;
                        else if (move.Contains("x") && currentX + 1 <= boundary.x) currentX++;
                    }
                    else
                    {
                        if (move.Contains("y") && currentY - 1 >= 0) currentY--;
                        else if (move.Contains("x") && currentX - 1 >= 0) currentX--;
                    }
                }
                else
                {
                    var compass = compassDict[currentPosition];
                    currentPosition = compass[dir];
                }

            }

            return $"{currentX} {currentY} {currentPosition}";
        }

        /// <summary>
        /// This function returns a Dictionary that on reading it, gives the direction a rover would face if turning left or right when facing a certain direction
        /// The key is the current direction the rover is facing and the value is another internal dictionary that has only 2 key value pairs
        /// The internal dictionary has a key for the instruction (L or R) and the value is the direction the rover would face based on its current position
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Dictionary<string, string>> GetCompass()
        {
            Dictionary<string, Dictionary<string, string>> compass = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, string> northCompass = new Dictionary<string, string>(){
                { "L", "W" }, { "R", "E" } };
            Dictionary<string, string> southCompass = new Dictionary<string, string>(){
                { "L", "E" }, { "R", "W" } };
            Dictionary<string, string> eastCompass = new Dictionary<string, string>(){
                { "L", "N" }, { "R", "S" } };
            Dictionary<string, string> westCompass = new Dictionary<string, string>(){
                { "L", "S" }, { "R", "N" } };

            compass.Add("N", northCompass);
            compass.Add("S", southCompass);
            compass.Add("E", eastCompass);
            compass.Add("W", westCompass);

            return compass;

        }

        /// <summary>
        /// This function returns a Dictionary that has values of how the rover would move given an M instruction facing a particular direction
        /// The key is the direction the rover is currently facing
        /// The value is the coordinate (x or y) that would be either increased or decreased
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetDirectionMovement()
        {
            Dictionary<string, string> movement = new Dictionary<string, string>()
            {
                {"N", "y+1" },{"S", "y-1" },{"W", "x-1" },{"E", "x+1" }
            };

            return movement;
        }
        
    }

    public class  Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //This indicates movement horizontally
        public int x { get; set; }

        //This indicates movement vertically
        public int y { get; set; }
    }

    public class RoverPosition
    {
        public RoverPosition(Coordinate coordinate, string position)
        {
            this.coordinate = coordinate;
            Position = position;
        }

        public Coordinate coordinate { get; set; }
        public string Position { get; set; }
    }
}
