
param(
    [parameter(Mandatory = $true)]
    $siteName,
    [parameter(Mandatory = $true)]
    $domainName
)
Set-ExecutionPolicy Bypass -Scope Process
Import-Module WebAdministration
$iisAppPoolDotNetVersion = "v4.0"


#removing website binding 
Remove-WebBinding -Name $domainName -IPAddress "*" -Port 80 -HostHeader $siteName

return "Domain Removed"
