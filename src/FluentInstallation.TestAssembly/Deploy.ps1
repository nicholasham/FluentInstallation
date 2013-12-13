
$path = Join-Path (Split-Path -Parent $MyInvocation.MyCommand.path) Bin
$moduleFile = @(Get-ChildItem $path  -Filter FluentInstallation.dll -Recurse )[0].FullName

Remove-Module FluentInstallation -ErrorAction SilentlyContinue
Import-Module $moduleFile

Write-Host "" -ForegroundColor Magenta

$DebugPreference = 'Continue' 
$WarningPreference = 'Continue' 
$VerbosePreference = 'Continue' 

Install-Fluent -AssemblyFile "FluentInstallation.TestAssembly.dll"  -Parameters @{}