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

        public string GetFinalCoordinates(string upperRightCoordinates, string roverPosition, string instruction)
        {
            var compassDict = GetCompass();
            var movementDict = GetDirectionMovement();

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

        public int x { get; set; }
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
