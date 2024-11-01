
param(
    [parameter(Mandatory = $true)]
    $siteName,
    [parameter(Mandatory = $true)]
    $domainName
)
Set-ExecutionPolicy Bypass -Scope Process
Import-Module WebAdministration
$iisAppPoolDotNetVersion = "v4.0"

#creating new website binding 
#site name e.g "mpc", domain name "testDomain"
New-WebBinding -Name $siteName -IPAddress "*" -Port 80 -HostHeader $domainName

return "Domain Created";
