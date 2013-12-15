
$path = Split-Path -Parent $MyInvocation.MyCommand.path
$moduleFile = @(Get-ChildItem $path  -Filter FluentInstallation.dll -Recurse )[0].FullName

$projectName = (Get-Item $path).Name 
$projectAssemblyFile = @(Get-ChildItem $path  -Filter "$($projectName).dll" -Recurse )[0].FullName

Remove-Module FluentInstallation -ErrorAction SilentlyContinue
Import-Module $moduleFile

Write-Host "" -ForegroundColor Magenta

$DebugPreference = 'Continue' 
$WarningPreference = 'Continue' 
$VerbosePreference = 'Continue' 

Install-Fluent -AssemblyFile $projectAssemblyFile  -Parameters @{}