
$path = Split-Path -Parent $MyInvocation.MyCommand.path

$projectBinPath = Join-Path $path "Bin"
$projectAssemblyFileName = "$OutputFileName$"
$projectAssemblyFile = @(Get-ChildItem $projectBinPath  -Filter $projectAssemblyFileName -Recurse )[0].FullName
$moduleFile = @(Get-ChildItem $projectBinPath  -Filter FluentInstallation.dll -Recurse )[0].FullName

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