
function Update-AssemblyBuildNumber {

	param(
		[parameter(Mandatory=$true, ValueFromPipeline = $true)]
		[ValidateNotNullOrEmpty()]
		[string] $assemblyFile,
		
		[parameter(Mandatory=$true, ValueFromPipeline = $true)]
		[ValidateNotNullOrEmpty()]
		[int] $buildNumber
	)

	$assemblyPattern = "[0-9]+(\.([0-9]+|\*)){1,3}"
    $assemblyVersionPattern = 'AssemblyVersion\("([0-9]+(\.([0-9]+|\*)){1,3})"\)'
                    
    $rawVersionNumberGroup = get-content $assemblyFile | select-string -pattern $assemblyVersionPattern | % { $_.Matches }            
    $rawVersionNumber = $rawVersionNumberGroup.Groups[1].Value
                  
    $versionParts = $rawVersionNumber.Split('.') 
    	
	$major = [int] $versionParts[0]
	$minor = [int] $versionParts[1]
	$revision = [int] $versionParts[2]
	$build = $buildNumber
	
    $updatedVersion = "{0}.{1}.{2}.{3}" -f $major, $minor, $revision, $build
	
	#Replace the version in each content item
	(Get-Content $assemblyFile | 
              Foreach-Object { $_ -replace $assemblyPattern, $updatedVersion }) |
                     Set-Content $assemblyFile -Force

	return $updatedVersion
			
}