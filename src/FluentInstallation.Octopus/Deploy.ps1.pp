
try{

$currentScriptDirectory = (Split-Path -Parent $MyInvocation.MyCommand.path)
$fluentInstallationScriptPath = Join-Path $currentScriptDirectory "FluentInstallation.ps1"
.$fluentInstallationScriptPath

$parameters =  @{}

if ($OctopusParameters -ne $null) {
$parameters = $OctopusParameters
}

Install -parameters $parameters

}
catch [Exception]
{
"Failed to run package script: $_.Exception.Message"
$_.Exception.StackTrace
Exit 1
}

