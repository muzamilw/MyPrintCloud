
param(
    [parameter(Mandatory = $true)]
    $siteName,

    [parameter(Mandatory = $true)]
    $sitePhysicalPath,

	[parameter(Mandatory = $true)]
    $siteOrganisationId

)



Set-ExecutionPolicy Bypass -Scope Process
Import-Module WebAdministration

return $sitePhysicalPath

$iisAppPoolDotNetVersion = "v4.0"

$mpcContentVirtualPath = "e:\mpc-content"

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $siteName -pathType container))
{
    #create the app pool
    $appPool = New-Item $siteName
    $appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
}

#navigate to the sites root
cd IIS:\Sites\

$virtualDirectoryName = "mpc-content"
$virtualDirectoryPath = "IIS:\Sites\$siteName\$virtualDirectoryName"
$resourceFileDirectory = "$mpcContentVirtualPath\Resources"
$OrganisationName = "Organisation" + $siteOrganisationId
$resourceFileDirectoryWithOrganisation = "$resourceFileDirectory\$OrganisationName"
$CopyResourceFolderPath = "$resourceFileDirectoryWithOrganisation\"

#check if the site exists
if (Test-Path $siteName -pathType container)
{
	if(!(Test-Path -Path $mpcContentVirtualPath )){
		# this command will create virtual folder
		New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $mpcContentVirtualPath
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Artworks" )){
		New-Item "$mpcContentVirtualPath\Artworks" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Attachments" )){
		New-Item "$mpcContentVirtualPath\Attachments" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Categories" )){
		New-Item "$mpcContentVirtualPath\Categories" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\CompanyBanners" )){
		New-Item "$mpcContentVirtualPath\CompanyBanners" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\CostCenres" )){
		New-Item "$mpcContentVirtualPath\CostCenres" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Designer" )){
		New-Item "$mpcContentVirtualPath\Designer" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Media" )){
		New-Item "$mpcContentVirtualPath\Media" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Organisations" )){
		New-Item "$mpcContentVirtualPath\Organisations" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Products" )){
		New-Item "$mpcContentVirtualPath\Products" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\SecondaryPages" )){
		New-Item "$mpcContentVirtualPath\SecondaryPages" -type directory
	}
	if(!(Test-Path -Path "$mpcContentVirtualPath\Stores" )){
		New-Item "$mpcContentVirtualPath\Stores" -type directory
	}
	if(!(Test-Path -Path $resourceFileDirectory ))
	{
		New-Item $resourceFileDirectory -type directory
		if(!(Test-Path -Path $resourceFileDirectoryWithOrganisation )){
		New-Item $resourceFileDirectoryWithOrganisation -type directory
		}
	}
	else if(!(Test-Path -Path $resourceFileDirectoryWithOrganisation )){
		New-Item $resourceFileDirectoryWithOrganisation -type directory
	}
	
    return "App already exists"
}

#create the site
$iisApp = New-Item $siteName -bindings @{protocol="http";bindingInformation=":80:" + $siteName} -physicalPath $sitePhysicalPath
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $siteName




#this command will create web app
New-WebApplication -Name mis -Site $siteName -PhysicalPath $sitePhysicalPath -ApplicationPool $appPool

# this command will create virtual folder
New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $mpcContentVirtualPath


# create resource directory
New-Item $resourceFileDirectory -type directory

#create all other directories
New-Item "$mpcContentVirtualPath\Artworks" -type directory
New-Item "$mpcContentVirtualPath\Attachments" -type directory
New-Item "$mpcContentVirtualPath\Categories" -type directory
New-Item "$mpcContentVirtualPath\CompanyBanners" -type directory
New-Item "$mpcContentVirtualPath\CostCenres" -type directory
New-Item "$mpcContentVirtualPath\Designer" -type directory
New-Item "$mpcContentVirtualPath\Media" -type directory
New-Item "$mpcContentVirtualPath\Organisations" -type directory
New-Item "$mpcContentVirtualPath\Products" -type directory
New-Item "$mpcContentVirtualPath\SecondaryPages" -type directory
New-Item "$mpcContentVirtualPath\Stores" -type directory

#create resource file folder with organisation
New-Item $resourceFileDirectoryWithOrganisation -type directory

#copies the english folder
Copy-Item -Path E:\Development\MyPrintCloud\MyPrintCloud.Cloud\MyPrintCloud\MPC.Web\MPC_Content\Resources\Organisation1\en-Us -Destination $CopyResourceFolderPath -recurse -Force

#copies the french folder
Copy-Item -Path E:\Development\MyPrintCloud\MyPrintCloud.Cloud\MyPrintCloud\MPC.Web\MPC_Content\Resources\Organisation1\fr-FR -Destination $CopyResourceFolderPath -recurse -Force

return "App Created"