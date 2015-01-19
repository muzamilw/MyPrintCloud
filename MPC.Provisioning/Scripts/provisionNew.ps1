
param(
    [parameter(Mandatory = $true)]
    $siteName,

    [parameter(Mandatory = $true)]
    $sitePhysicalPath

    

)


Set-ExecutionPolicy Bypass -Scope Process
Import-Module WebAdministration

$iisAppPoolDotNetVersion = "v4.0"



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

#check if the site exists
if (Test-Path $siteName -pathType container)
{
	if(!(Test-Path -Path $virtualDirectoryPath )){
		# this command will create virtual folder
		New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $sitePhysicalPath
	}
    return "App already exists"
}

#create the site
$iisApp = New-Item $siteName -bindings @{protocol="http";bindingInformation=":80:" + $siteName} -physicalPath $sitePhysicalPath
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $siteName




#this command will create web app
New-WebApplication -Name mis -Site $siteName -PhysicalPath $sitePhysicalPath -ApplicationPool $appPool

# this command will create virtual folder
New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $sitePhysicalPath

$resourceFileDirectory = "IIS:\Sites\$siteName\$virtualDirectoryPath"

#New-Item $resourceFileDirectory -type directory -physicalPath $sitePhysicalPath
#New-Item -ItemType directory -Path $resourceFileDirectory
#New-Item -ItemType Directory -Path $resourceFileDirectory -Force
New-Item -Path "$resourceFileDirectory"  -ItemType Directory 
return "App Created" + $siteName