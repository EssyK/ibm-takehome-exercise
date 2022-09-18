$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$exePath = "$scriptPath\MarsRover.exe"
$arguments = "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"

& $exePath $arguments