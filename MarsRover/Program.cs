using System;
using System.Collections.Generic;

namespace MarsRover
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            RoverNavigation roverNavigation = new RoverNavigation(args);
            if(roverNavigation.IsValid) roverNavigation.Navigate();            
        }
    }


}
