﻿
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

function Install()
{
	param(
		[hashtable] $parameters
	)
	
	Install-Fluent -AssemblyFile $projectAssemblyFile  -Parameters $parameters
	
}


function Uninstall()
{
	param(
		[hashtable] $parameters
	)
	
	Uninstall-Fluent -AssemblyFile $projectAssemblyFile  -Parameters $parameters
	
}