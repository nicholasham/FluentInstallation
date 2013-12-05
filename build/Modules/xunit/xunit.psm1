
$path = (Split-Path -Parent $MyInvocation.MyCommand.path)
$xunit = Join-Path $path "xunit.console.clr4.x86.exe"

function Run-Xunit {
	[CmdletBinding()]
	param(
		[Parameter(Position=0,Mandatory=1)]$file
	)
	
	Write-Output "Launching xunit for $file"
	exec { & $xunit "$file"}
}