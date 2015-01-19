
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

#check if the site exists
if (Test-Path $siteName -pathType container)
{
    return "App already exists"
}

#create the site
$iisApp = New-Item $siteName -bindings @{protocol="http";bindingInformation=":80:" + $siteName} -physicalPath $sitePhysicalPath
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $siteName
$virtualDirectoryName = "mpc-content"

$virtualDirectoryPath = "IIS:\Sites\$siteName\$virtualDirectoryName"

#New-WebApplication -Name $siteName -ApplicationPool "applicationPool" -PhysicalPath $sitePhysicalPath -Site $virtualDirectoryName

New-WebApplication "visrta" -Site $siteName -ApplicationPool "applicationPools" -
PhysicalPath $sitePhysicalPath
#New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $sitePhysicalPath
# Init
#IIS:\>New-WebVirtualDirectory -Site $siteName -Name "mpc-content" -PhysicalPath "E:\Development"

return "App Created" + $siteName