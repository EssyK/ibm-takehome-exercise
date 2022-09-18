# Mars Rover Design
The Solution is a console App and has been developed in C# using visual studio and has 3 folders.
The code base and test are in the following folders respectively: MarsRover and MarsRoverTest
The build for execution is in the MarsRoverBuild folder. I have provide a powershell script, ExecuteCommand.ps1 for invoking the program. 

Given the following:
1. The rover position and location are represented by a combination of x and y coordinaates and a letter representing (NORTH, South, East, West)
2. The rover is controlled by sending a string of letters. The possible letters are L, R and M
3. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot. 'M' means move forward one grid point, 
and maintain the same heading.
4. The plateau is divided into a grid to simplify navigation

Assumptions:
1. The lower-left coordinates of the plateau are assumed to be 0, 0.
2. The upper right coordinates and the lower left are boundaries beyond which the rover should not go
3. The square directly North from (x, y) is (x, y+1).
4. If the instructions sent to the rover would cause the rover to go outside the boundary, the coordinate will not change and the rover will retain its position.
5. The input is not empty

Approach
1. Have two dictionaries which have constant look up time. These dictionaries are created once before navigation and are constant.
2. One dictionary will contain the resultant cardinal direction the rover would face if it received a L or R instruction
3. Another dictionary will contain the movement a rover should make maintaining the same heading.
4. Iteratively go through the input retrieving the rover position and instruction
