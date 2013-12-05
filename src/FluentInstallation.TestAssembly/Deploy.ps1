
Remove-Module FluentInstallation -ErrorAction SilentlyContinue
Import-Module "D:\Projects\github\FluentInstallation\src\FluentInstallation.TestAssembly\bin\Debug\FluentInstallation.dll"

Write-Host "" -ForegroundColor Magenta

$DebugPreference = 'Continue' 
$WarningPreference = 'Continue' 
$VerbosePreference = 'Continue' 

Install-Fluent -AssemblyFile "D:\Projects\github\FluentInstallation\src\FluentInstallation.TestAssembly\bin\Debug\FluentInstallation.TestAssembly.dll" 