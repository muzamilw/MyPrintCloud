
param(
    [parameter(Mandatory = $true)]
    $siteName,

    [parameter(Mandatory = $true)]
    $sitePhysicalPath,

	[parameter(Mandatory = $true)]
    $siteOrganisationId,

	[parameter(Mandatory = $true)]
    $mpcContentFolder,

	[parameter(Mandatory = $true)]
    $misFolder

)

Set-ExecutionPolicy Bypass -Scope Process
Import-Module WebAdministration


$iisAppPoolDotNetVersion = "v4.0"

#navigate to the app pools root
cd IIS:\AppPools\


$appPool = ""

#check if the app pool exists
if (!(Test-Path $siteName -pathType container))
{
    #create the app pool
    $appPool = New-Item $siteName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
	$appPool.processModel.identityType = "NetworkService"
	$appPool | set-item 

}
else
{
	$appPool = Get-Item "IIS:\Sites\"+$siteName | Select-Object applicationPool
}

#navigate to the sites root
cd IIS:\Sites\

$virtualDirectoryName = "mpc-content"
$virtualDirectoryPath = "IIS:\Sites\$siteName\$virtualDirectoryName"
$resourceFileDirectory = "$mpcContentFolder\Resources"
$OrganisationName = $siteOrganisationId
$resourceFileDirectoryWithOrganisation = "$resourceFileDirectory\$OrganisationName"
$CopyResourceFolderPath = "$resourceFileDirectoryWithOrganisation\"



#creating new website with binding information and setting it's poolname
$iisApp = New-Item $siteName -bindings @{protocol="http";bindingInformation=":80:" + $siteName} -physicalPath $sitePhysicalPath
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $siteName
#$iisApp | set-item 

"$($assoc.Id) - $($assoc.Name) - $($assoc.Owner)"

#this command will create MIS virtual directory/App
New-WebApplication -Name mis -Site $siteName -PhysicalPath $misFolder -ApplicationPool $siteName


# this command will create MPCContent virtual folder
New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $mpcContentFolder


# create resource directory, root folder
New-Item $resourceFileDirectory -type directory



$englishFolder = (Get-Item -Path ".\" -Verbose).FullName + "\en-Us"
$frenchFolder = (Get-Item -Path ".\" -Verbose).FullName + "\fr-FR"

#copies the english folder
Copy-Item -Path $englishFolder -Destination $CopyResourceFolderPath -recurse -Force

#copies the french folder
Copy-Item -Path $frenchFolder -Destination $CopyResourceFolderPath -recurse -Force

return "App Created"


