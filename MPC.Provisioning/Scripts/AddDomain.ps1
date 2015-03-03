
param(
    [parameter(Mandatory = $true)]
    $siteName,
)

Set-ExecutionPolicy Bypass -Scope Process
Import-Module WebAdministration
$iisAppPoolDotNetVersion = "v4.0"


#navigate to the sites root
cd IIS:\Sites\ | out-null

#creating new website with binding information and setting it's poolname
$iisApp = New-Item $siteName -bindings @{protocol="http";bindingInformation=":80:" + $siteName} 
$iisApp | Set-ItemProperty -Name "applicationPool" -Value $siteName
#$iisApp | set-item 