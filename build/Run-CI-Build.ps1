Clear-Host

$path = (Split-Path -Parent $MyInvocation.MyCommand.path)

$buildModulePath = Join-Path $path Modules

if ( !($env:PSModulePath -match  [regex]::escape($buildModulePath)))
{
	$env:PSModulePath = $env:PSModulePath + ";" +  $buildModulePath
}

Remove-Module psake -ErrorAction SilentlyContinue
Import-Module psake

$psake.use_exit_on_error = $true

$buildFile = (Join-Path $path Build.ps1)
Invoke-psake -buildFile $buildFile -taskList Run-CI-Build 


