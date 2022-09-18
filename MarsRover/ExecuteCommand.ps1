$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$marsRoverExePath = "$scriptPath\MarsRover.exe"

# This is the sample input
$inputArguments = "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"

& $marsRoverExePath $inputArguments