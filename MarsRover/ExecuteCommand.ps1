[string]$exePath = "C:\Path\To\Some.exe"
$psi = New-Object System.Diagnostics.ProcessStartInfo $exePath
$psi.Arguments = "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM"
[System.Diagnostics.Process]::Start($psi)