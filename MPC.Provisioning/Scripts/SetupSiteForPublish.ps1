
param(
    [parameter(Mandatory = $false)]
    $siteName,

    [parameter(Mandatory = $false)]
    $sitePhysicalPath,

    [parameter(Mandatory = $false)]
    $siteAppPoolName,

    [parameter(Mandatory = $false)]
    [int]$sitePort,

    [parameter(Mandatory = $false)]
    $deploymentUserName,

    [parameter(Mandatory = $false)]
    $deploymentUserPassword,

    [parameter(Mandatory = $false)]
    $managedRunTimeVersion,

    [parameter(Mandatory = $false)]
    $publishSettingSavePath,

    [parameter(Mandatory = $false)]
    $publishSettingFileName
)

Import-LocalizedData -BindingVariable Resources -FileName Resources.psd1

# ==================================


 #constants
 $SCRIPTERROR = 0
 $WARNING = 1
 $INFO = 2
 $logfile = ".\HostingLog-$(get-date -format MMddyyHHmmss).log"

$template = @"
<?xml version="1.0" encoding="utf-8"?>
<publishData>
  <publishProfile
    publishUrl=""
    msdeploySite=""
    destinationAppUrl=""
    mySQLDBConnectionString=""
    SQLServerDBConnectionString=""
    profileName="Default Settings"
    publishMethod="MSDeploy"
    userName=""
    userPWD=""
    savePWD="True"
    />
</publishData>
"@

#the order is important. Check for apppool name first. Its possible that
#the user gave just a sitename to set permissions. In this case leave apppool emtpy.
#else give a default name to the apppool.
if(!$siteAppPoolName)
{
    if(!$siteName)
    {
        $siteAppPoolName = "WDeployAppPool"
    }
}
else
{
    $siteAppPoolName = $siteAppPoolName.Trim()
}

#now the sitename check. If its empty give it a default name
if(!$siteName)
{
    $siteName = "WDeploySite"
}
else
{
    $siteName = $siteName.Trim()
}

if(!$sitePhysicalPath)
{
    $sitePhysicalPath =  $env:SystemDrive + "\inetpub\WDeploySite"
}
else
{
    $sitePhysicalPath = $sitePhysicalPath.Trim()
}

#global variable. Because we need to return two values from MWA from one function. [REF] has bugs. Hence global
$global:sitePath = $sitePhysicalPath
$global:publishURL = $null

# this function does logging
function write-log([int]$type, [string]$info){

    $message = $info -f $args
    $logMessage = get-date -format HH:mm:ss

    Switch($type)
    {
        $SCRIPTERROR
        {
            $logMessage = $logMessage + "`t" + $Resources.Error + "`t" +  $message
            write-host -foregroundcolor white -backgroundcolor red $logMessage
        }
        $WARNING
        {
            $logMessage = $logMessage + "`t" + $Resources.Warning + "`t" +  $message
            write-host -foregroundcolor black -backgroundcolor yellow $logMessage
        }
        default
        {
            $logMessage = $logMessage + "`t" + $Resources.Info + "`t" +  $message
            write-host -foregroundcolor black -backgroundcolor green  $logMessage
        }
    }

    $logMessage >> $logfile
}


function GetPublishSettingSavePath()
{
    if(!$publishSettingFileName)
    {
        $publishSettingFileName = "WDeploy.PublishSettings"
    }

    if(!$publishSettingSavePath)
    {
        $publishSettingSavePath = [System.Environment]::GetFolderPath("Desktop")
    }

    if((test-path $publishSettingSavePath) -eq $false)
    {
        write-log $SCRIPTERROR $Resources.FailedToAccessScriptsFolder $publishSettingSavePath
        return $null
    }

    return Join-Path $publishSettingSavePath $publishSettingFileName
}

# returns false if OS is not server SKU
function NotServerOS
{
    $sku = $((gwmi win32_operatingsystem).OperatingSystemSKU)
    $server_skus = @(7,8,9,10,12,13,14,15,17,18,19,20,21,22,23,24,25)

    return ($server_skus -notcontains $sku)
}

# gives a user access to an IIS site's scope
function GrantAccessToSiteScope($username, $websiteName)
{
    trap [Exception]
    {
        write-log $SCRIPTERROR $Resources.FailedToGrantUserAccessToSite $username $websiteName
        return $false
    }

    foreach($mInfo in [Microsoft.Web.Management.Server.ManagementAuthorization]::GetAuthorizedUsers($websiteName, $false, 0,[int]::MaxValue))
    {
        if($mInfo.Name -eq $username)
        {
            write-log $INFO $Resources.UserHasAccessToSite $username $websiteName
            return $true
        }
    }

    [Microsoft.Web.Management.Server.ManagementAuthorization]::Grant($username, $websiteName, $FALSE) | out-null
    write-log $INFO $Resources.GrantedUserAccessToSite $username $websiteName
    return $true
}

# gives a user permissions to a file on disk
function GrantPermissionsOnDisk($username, $type, $options)
{
    trap [Exception]
    {
        write-log $SCRIPTERROR $Resources.NotGrantedPermissions $type $username $global:sitePath
    }

    $acl = (Get-Item $global:sitePath).GetAccessControl("Access")
    $accessrule = New-Object system.security.AccessControl.FileSystemAccessRule($username, $type, $options, "None", "Allow")
    $acl.AddAccessRule($accessrule)
    set-acl -aclobject $acl $global:sitePath
    write-log $INFO $Resources.GrantedPermissions $type $username $global:sitePath
}

function AddUser($username, $password)
{
    if(-not (CheckLocalUserExists($username) -eq $true))
    {
        $comp = [adsi] "WinNT://$env:computername,computer"
        $user = $comp.Create("User", $username)
        $user.SetPassword($password)
        $user.SetInfo()
        write-log $INFO $Resources.CreatedUser $username
    }
}

function CheckLocalUserExists($username)
{
    $objComputer = [ADSI]("WinNT://$env:computername")
    $colUsers = ($objComputer.psbase.children | Where-Object {$_.psBase.schemaClassName -eq "User"} | Select-Object -expand Name)

    $blnFound = $colUsers -contains $username

    if ($blnFound)
    {
        return $true
    }
    else
    {
        return $false
    }
}

function CheckIfUserIsAdmin($username)
{
    $computer = [ADSI]("WinNT://$env:computername,computer")
    $group = $computer.psbase.children.find("Administrators")

    $colMembers = $group.psbase.invoke("Members") | %{$_.GetType().InvokeMember("Name",'GetProperty',$null,$_,$null)}

    $bIsMember = $colMembers -contains $username
    if($bIsMember)
    {
        return $true
    }
    else
    {
        return $false
    }
}

function CreateLocalUser($username, $password, $isAdmin)
{
    AddUser $username $password

    if($isAdmin)
    {
        if(-not(CheckIfUserIsAdmin($username) -eq $true))
        {
            $group = [ADSI]"WinNT://$env:computername/Administrators,group"
            $group.add("WinNT://$env:computername/$username")
            write-log $INFO $Resources.AddedUserAsAdmin $username
        }
        else
        {
            write-log $INFO $Resources.IsAdmin $username
        }
    }

    return $true
}

function Initialize
{
    trap [Exception]
    {
        write-log $SCRIPTERROR $Resources.CheckIIS7Installed
        break
    }

    $inetsrvPath = ${env:windir} + "\system32\inetsrv\"

    [System.Reflection.Assembly]::LoadFrom( $inetsrvPath + "Microsoft.Web.Administration.dll" ) > $null
    [System.Reflection.Assembly]::LoadFrom( $inetsrvPath + "Microsoft.Web.Management.dll" )   > $null
}

function GetPublicHostname()
{
    $ipProperties = [System.Net.NetworkInformation.IPGlobalProperties]::GetIPGlobalProperties()
    if($ipProperties.DomainName -eq "")
    {
        return $ipProperties.HostName
    }
    else
    {
        return "{0}.{1}" -f $ipProperties.HostName, $ipProperties.DomainName
    }
}

function GenerateStrongPassword()
{
   [System.Reflection.Assembly]::LoadWithPartialName("System.Web") > $null
   return [System.Web.Security.Membership]::GeneratePassword(12,4)
}

function GetPublishURLFromBindingInfo($bindingInfo, $protocol, $hostname)
{
    $port = 80
    trap [Exception]
    {
        #return defaults
        return "http://$hostname"
    }

    if(($bindingInfo -match "(.*):(\d*):([^:]*)$") -and
        ($Matches.Count -eq 4 ))
    {
        $port = $Matches[2]
        $header = $Matches[3]
        $ipaddress = $Matches[1]
        if($header)
        {
            $hostname = $header
        }
        elseif(($ipaddress) -AND (-not($ipaddress -eq "*")))
        {
            $bracketsArray = @('[',']')
            $hostname  = $ipaddress.Trim($bracketsArray)
        }

        if(-not($port -eq 80))
        {
            $hostname = $hostname + ":" + $port
        }
    }

    return $protocol + "://" + $hostname
}


function GetUnusedPortForSiteBinding()
{
    [int[]] $portArray = $null
    $serverManager = (New-Object Microsoft.Web.Administration.ServerManager)
    foreach($site in $serverManager.Sites)
    {
        foreach($binding in $site.Bindings)
        {
            if($binding.IsIPPortHostBinding)
            {
                if($binding.Protocol -match "https?")
                {
                    if(($binding.BindingInformation -match "(.*):(\d*):([^:]*)$") -and
                    ($Matches.Count -eq 4 ))
                    {
                        $portArray = $portArray + $Matches[2]
                    }
                }
            }
        }
    }

    if(-not($portArray -eq $null))
    {
        $testPortArray = 8080..8200
        foreach($port in $testPortArray)
        {
            if($portArray -notcontains $port)
            {
                return $port
            }
        }
    }

    return 8081 #default
}

function CreateSite($name, $appPoolName, $port, $dotnetVersion)
{
    trap [Exception]
    {
        write-log $SCRIPTERROR $Resources.SiteCreationFailed
        return $false
    }

    $hostname = GetPublicHostName
    $global:publishURL = "http://$hostname"
    if(-not($port -eq 80))
    {
        $global:publishURL = $global:publishURL + ":" + $port
    }

    $configHasChanges = $false
    $serverManager = (New-Object Microsoft.Web.Administration.ServerManager)

    #appPool might be empty. WHen the user gave just a site name to
    #set the permissions on. As long as the sitename is not empty
    if($appPoolName)
    {
        $appPool = $serverManager.ApplicationPools[$appPoolName]
        if ($appPool -eq $null)
        {
            $appPool = $serverManager.ApplicationPools.Add($appPoolName)
            $appPool.Enable32BitAppOnWin64 = $true

            if( ($dotnetVersion) -and
            (CheckVersionWithinAllowedRange $dotnetVersion) )
            {
                $appPool.ManagedRuntimeVersion = $dotnetVersion
            }
            $configHasChanges = $true
            write-log $INFO $Resources.AppPoolCreated $appPoolName
        }
        else
        {
            write-log $WARNING $Resources.AppPoolExists $appPoolName
        }
    }

    $newSite = $serverManager.Sites[$name]
    if ($newSite -eq $null)
    {
        $newSite = $serverManager.Sites.Add($name,$global:sitePath, $port)
        if($appPool)
        {
            $newSite.Applications[0].ApplicationPoolName = $appPool.Name
        }

        if((test-path $global:sitePath) -eq $false)
        {
            [System.IO.Directory]::CreateDirectory($global:sitePath)
        }
        else
        {
            write-log $WARNING $Resources.SiteVirtualDirectoryExists $global:sitePath
        }

        $newSite.ServerAutoStart = $true
        $configHasChanges = $true
        write-log $INFO $Resources.SiteCreated $name
    }
    else
    {
        #get virtual directory and siteport
        $global:sitePath = [System.Environment]::ExpandEnvironmentVariables($newSite.Applications["/"].VirtualDirectories["/"].PhysicalPath)

        foreach($binding in $newSite.Bindings)
        {
            if($binding.IsIPPortHostBinding)
            {
                if($binding.Protocol -match "https?")
                {
                    $global:publishURL = GetPublishURLFromBindingInfo $binding.BindingInformation $binding.Protocol $hostname
                }
            }
        }

        if($appPoolName)
        {
            if (-not($newSite.Applications[0].ApplicationPoolName -eq $appPool.Name ))
            {
                $newSite.Applications[0].ApplicationPoolName = $appPool.Name
                $configHasChanges = $true
                write-log $INFO $Resources.SiteAppPoolUpdated $name $appPoolName
            }
            else
            {
                write-log $INFO $Resources.SiteExists $name $appPoolName
            }
        }
        else
        {
            write-log $INFO $Resources.SiteExists $name $newSite.Applications[0].ApplicationPoolName
        }
    }

    if ($configHasChanges)
    {
        $serverManager.CommitChanges()
    }

    return $true
}

function CheckUserViaLogon($username, $password)
{

 $signature = @'
    [DllImport("advapi32.dll")]
    public static extern int LogonUser(
        string lpszUserName,
        string lpszDomain,
        string lpszPassword,
        int dwLogonType,
        int dwLogonProvider,
        ref IntPtr phToken);
'@

    $type = Add-Type -MemberDefinition $signature  -Name Win32Utils -Namespace LogOnUser  -PassThru

    [IntPtr]$token = [IntPtr]::Zero

    $value = $type::LogOnUser($username, $env:computername, $password, 2, 0, [ref] $token)

    if($value -eq 0)
    {
        return $false
    }

    return $true
 }

function CheckUsernamePasswordCombination($user, $password)
{
    if(($user) -AND ($password))
    {
        if(CheckLocalUserExists($user) -eq $true)
        {
            if(CheckUserViaLogon $user $password)
            {
                return $true
            }
            else
            {
                write-Log $SCRIPTERROR $Resources.FailedToValidateUserWithSpecifiedPassword $user
                return $false
            }
        }
    }

    return $true
}

function CreateProfileXml([string]$nameofSite, [string]$username, $password, [string]$hostname, $pathToSaveFile)
{
    trap [Exception]
    {
        write-log $SCRIPTERROR $Resources.FailedToWritePublishSettingsFile $pathToSaveFile
        return
    }

    $xml = New-Object xml

    if(Test-Path $pathToSaveFile)
    {
        $xml.Load($pathToSaveFile)
    }
    else
    {
        $xml.LoadXml($template)
    }

    $newProfile = (@($xml.publishData.publishProfile)[0])
    $newProfile.publishUrl = $hostname
    $newProfile.msdeploySite = $nameofSite

    $newProfile.destinationAppUrl = $global:publishURL.ToString()
    $newProfile.userName = $username

    if(-not ($password -eq $null))
    {
        $newProfile.userPWD = $password.ToString()
    }
    else
    {
        write-log $WARNING $Resources.NoPasswordForExistingUserForPublish
    }

    $xml.Save($pathToSaveFile)

    write-log $INFO $Resources.SavingPublishXmlToPath $pathToSaveFile
}

function CheckVersionWithinAllowedRange($managedVersion)
{
    trap [Exception]
    {
        return $false
    }

    $KeyPath = "HKLM:\Software\Microsoft\.NETFramework"
    $key = Get-ItemProperty -path $KeyPath
    $path = $key.InstallRoot
    $files = [System.IO.Directory]::GetFiles($path, "mscorlib.dll", [System.IO.SearchOption]::AllDirectories)
    foreach($file in $files)
    {
        if($file -match "\\(v\d\.\d).\d*\\")
        {
            if($Matches[1] -eq $managedVersion)
            {
                return $true
            }
        }
    }
    return $false
}


#================= Main Script =================

if(NotServerOS)
{
    write-log $SCRIPTERROR $Resources.NotServerOS
    break
}

Initialize
if(CheckUsernamePasswordCombination $deploymentUserName $deploymentUserPassword)
{
    if(!$sitePort)
    {
        $sitePort = GetUnusedPortForSiteBinding
    }
    if(CreateSite $siteName $siteAppPoolName $sitePort $managedRunTimeVersion)
    {
        if(!$deploymentUserName)
        {
            $idx = $siteName.IndexOf(' ')
            if( ($idx -gt 0) -or ($siteName.Length -gt 16))
            {
                $deploymentUserName = "WDeployuser"
            }
            else
            {
                $deploymentUserName = $siteName + "user"
            }
        }

        if( (CheckLocalUserExists($deploymentUserName) -eq $true))
        {
            $deploymentUserPassword = $null
        }
        else
        {
            if(!$deploymentUserPassword)
            {
                $deploymentUserPassword = GenerateStrongPassword
            }
        }

        if(CreateLocalUser $deploymentUserName $deploymentUserPassword $false)
        {
            GrantPermissionsOnDisk $deploymentUserName "FullControl" "ContainerInherit,ObjectInherit"

            if(GrantAccessToSiteScope ($env:computername + "\" + $deploymentUserName) $siteName)
            {
                $hostname = GetPublicHostName
                $savePath = GetPublishSettingSavePath
                if($savePath)
                {
                    CreateProfileXml $siteName $deploymentUserName $deploymentUserPassword $hostname $savePath
                }
            }
        }
    }
}

# SIG # Begin signature block
# MIIanQYJKoZIhvcNAQcCoIIajjCCGooCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQU4AUtviQq7SNFU/aDJqnC6g+j
# QX6gghWCMIIEwzCCA6ugAwIBAgITMwAAADQkMUDJoMF5jQAAAAAANDANBgkqhkiG
# 9w0BAQUFADB3MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
# A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSEw
# HwYDVQQDExhNaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EwHhcNMTMwMzI3MjAwODI1
# WhcNMTQwNjI3MjAwODI1WjCBszELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hp
# bmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jw
# b3JhdGlvbjENMAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNO
# OkI4RUMtMzBBNC03MTQ0MSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBT
# ZXJ2aWNlMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5RoHrQqWLNS2
# NGTLNCDyvARYgou1CdxS1HCf4lws5/VqpPW2LrGBhlkB7ElsKQQe9TiLVxj1wDIN
# 7TSQ7MZF5buKCiWq76F7h9jxcGdKzWrc5q8FkT3tBXDrQc+rsSVmu6uitxj5eBN4
# dc2LM1x97WfE7QP9KKxYYMF7vYCNM5NhYgixj1ESZY9BfsTVJektZkHTQzT6l4H4
# /Ieh7TlSH/jpPv9egMkGNgfb27lqxzfPhrUaS0rUJfLHyI2vYWeK2lMv80wegyxj
# yqAQUhG6gVhzQoTjNLLu6pO+TILQfZYLT38vzxBdGkVmqwLxXyQARsHBVdKDckIi
# hjqkvpNQAQIDAQABo4IBCTCCAQUwHQYDVR0OBBYEFF9LQt4MuTig1GY2jVb7dFlJ
# ZoErMB8GA1UdIwQYMBaAFCM0+NlSRnAK7UD7dvuzK7DDNbMPMFQGA1UdHwRNMEsw
# SaBHoEWGQ2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3Rz
# L01pY3Jvc29mdFRpbWVTdGFtcFBDQS5jcmwwWAYIKwYBBQUHAQEETDBKMEgGCCsG
# AQUFBzAChjxodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY3Jv
# c29mdFRpbWVTdGFtcFBDQS5jcnQwEwYDVR0lBAwwCgYIKwYBBQUHAwgwDQYJKoZI
# hvcNAQEFBQADggEBAA9CUKDVHq0XPx8Kpis3imdYLbEwTzvvwldp7GXTTMVQcvJz
# JfbkhALFdRxxWEOr8cmqjt/Kb1g8iecvzXo17GbX1V66jp9XhpQQoOtRN61X9id7
# I08Z2OBtdgQlMGESraWOoya2SOVT8kVOxbiJJxCdqePPI+l5bK6TaDoa8xPEFLZ6
# Op5B2plWntDT4BaWkHJMrwH3JAb7GSuYslXMep/okjprMXuA8w6eV4u35gW2OSWa
# l4IpNos4rq6LGqzu5+wuv0supQc1gfMTIOq0SpOev5yDVn+tFS9cKXELlGc4/DC/
# Zef1Od7qIu2HjKuyO7UBwq3g/I4lFQwivp8M7R0wggTsMIID1KADAgECAhMzAAAA
# ymzVMhI1xOFVAAEAAADKMA0GCSqGSIb3DQEBBQUAMHkxCzAJBgNVBAYTAlVTMRMw
# EQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVN
# aWNyb3NvZnQgQ29ycG9yYXRpb24xIzAhBgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNp
# Z25pbmcgUENBMB4XDTE0MDQyMjE3MzkwMFoXDTE1MDcyMjE3MzkwMFowgYMxCzAJ
# BgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25k
# MR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xDTALBgNVBAsTBE1PUFIx
# HjAcBgNVBAMTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjCCASIwDQYJKoZIhvcNAQEB
# BQADggEPADCCAQoCggEBAJZxXe0GRvqEy51bt0bHsOG0ETkDrbEVc2Cc66e2bho8
# P/9l4zTxpqUhXlaZbFjkkqEKXMLT3FIvDGWaIGFAUzGcbI8hfbr5/hNQUmCVOlu5
# WKV0YUGplOCtJk5MoZdwSSdefGfKTx5xhEa8HUu24g/FxifJB+Z6CqUXABlMcEU4
# LYG0UKrFZ9H6ebzFzKFym/QlNJj4VN8SOTgSL6RrpZp+x2LR3M/tPTT4ud81MLrs
# eTKp4amsVU1Mf0xWwxMLdvEH+cxHrPuI1VKlHij6PS3Pz4SYhnFlEc+FyQlEhuFv
# 57H8rEBEpamLIz+CSZ3VlllQE1kYc/9DDK0r1H8wQGcCAwEAAaOCAWAwggFcMBMG
# A1UdJQQMMAoGCCsGAQUFBwMDMB0GA1UdDgQWBBQfXuJdUI1Whr5KPM8E6KeHtcu/
# gzBRBgNVHREESjBIpEYwRDENMAsGA1UECxMETU9QUjEzMDEGA1UEBRMqMzE1OTUr
# YjQyMThmMTMtNmZjYS00OTBmLTljNDctM2ZjNTU3ZGZjNDQwMB8GA1UdIwQYMBaA
# FMsR6MrStBZYAck3LjMWFrlMmgofMFYGA1UdHwRPME0wS6BJoEeGRWh0dHA6Ly9j
# cmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY0NvZFNpZ1BDQV8w
# OC0zMS0yMDEwLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKGPmh0dHA6
# Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljQ29kU2lnUENBXzA4LTMx
# LTIwMTAuY3J0MA0GCSqGSIb3DQEBBQUAA4IBAQB3XOvXkT3NvXuD2YWpsEOdc3wX
# yQ/tNtvHtSwbXvtUBTqDcUCBCaK3cSZe1n22bDvJql9dAxgqHSd+B+nFZR+1zw23
# VMcoOFqI53vBGbZWMrrizMuT269uD11E9dSw7xvVTsGvDu8gm/Lh/idd6MX/YfYZ
# 0igKIp3fzXCCnhhy2CPMeixD7v/qwODmHaqelzMAUm8HuNOIbN6kBjWnwlOGZRF3
# CY81WbnYhqgA/vgxfSz0jAWdwMHVd3Js6U1ZJoPxwrKIV5M1AHxQK7xZ/P4cKTiC
# 095Sl0UpGE6WW526Xxuj8SdQ6geV6G00DThX3DcoNZU6OJzU7WqFXQ4iEV57MIIF
# vDCCA6SgAwIBAgIKYTMmGgAAAAAAMTANBgkqhkiG9w0BAQUFADBfMRMwEQYKCZIm
# iZPyLGQBGRYDY29tMRkwFwYKCZImiZPyLGQBGRYJbWljcm9zb2Z0MS0wKwYDVQQD
# EyRNaWNyb3NvZnQgUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkwHhcNMTAwODMx
# MjIxOTMyWhcNMjAwODMxMjIyOTMyWjB5MQswCQYDVQQGEwJVUzETMBEGA1UECBMK
# V2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
# IENvcnBvcmF0aW9uMSMwIQYDVQQDExpNaWNyb3NvZnQgQ29kZSBTaWduaW5nIFBD
# QTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALJyWVwZMGS/HZpgICBC
# mXZTbD4b1m/My/Hqa/6XFhDg3zp0gxq3L6Ay7P/ewkJOI9VyANs1VwqJyq4gSfTw
# aKxNS42lvXlLcZtHB9r9Jd+ddYjPqnNEf9eB2/O98jakyVxF3K+tPeAoaJcap6Vy
# c1bxF5Tk/TWUcqDWdl8ed0WDhTgW0HNbBbpnUo2lsmkv2hkL/pJ0KeJ2L1TdFDBZ
# +NKNYv3LyV9GMVC5JxPkQDDPcikQKCLHN049oDI9kM2hOAaFXE5WgigqBTK3S9dP
# Y+fSLWLxRT3nrAgA9kahntFbjCZT6HqqSvJGzzc8OJ60d1ylF56NyxGPVjzBrAlf
# A9MCAwEAAaOCAV4wggFaMA8GA1UdEwEB/wQFMAMBAf8wHQYDVR0OBBYEFMsR6MrS
# tBZYAck3LjMWFrlMmgofMAsGA1UdDwQEAwIBhjASBgkrBgEEAYI3FQEEBQIDAQAB
# MCMGCSsGAQQBgjcVAgQWBBT90TFO0yaKleGYYDuoMW+mPLzYLTAZBgkrBgEEAYI3
# FAIEDB4KAFMAdQBiAEMAQTAfBgNVHSMEGDAWgBQOrIJgQFYnl+UlE/wq4QpTlVnk
# pDBQBgNVHR8ESTBHMEWgQ6BBhj9odHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtp
# L2NybC9wcm9kdWN0cy9taWNyb3NvZnRyb290Y2VydC5jcmwwVAYIKwYBBQUHAQEE
# SDBGMEQGCCsGAQUFBzAChjhodHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2Nl
# cnRzL01pY3Jvc29mdFJvb3RDZXJ0LmNydDANBgkqhkiG9w0BAQUFAAOCAgEAWTk+
# fyZGr+tvQLEytWrrDi9uqEn361917Uw7LddDrQv+y+ktMaMjzHxQmIAhXaw9L0y6
# oqhWnONwu7i0+Hm1SXL3PupBf8rhDBdpy6WcIC36C1DEVs0t40rSvHDnqA2iA6VW
# 4LiKS1fylUKc8fPv7uOGHzQ8uFaa8FMjhSqkghyT4pQHHfLiTviMocroE6WRTsgb
# 0o9ylSpxbZsa+BzwU9ZnzCL/XB3Nooy9J7J5Y1ZEolHN+emjWFbdmwJFRC9f9Nqu
# 1IIybvyklRPk62nnqaIsvsgrEA5ljpnb9aL6EiYJZTiU8XofSrvR4Vbo0HiWGFzJ
# NRZf3ZMdSY4tvq00RBzuEBUaAF3dNVshzpjHCe6FDoxPbQ4TTj18KUicctHzbMrB
# 7HCjV5JXfZSNoBtIA1r3z6NnCnSlNu0tLxfI5nI3EvRvsTxngvlSso0zFmUeDord
# EN5k9G/ORtTTF+l5xAS00/ss3x+KnqwK+xMnQK3k+eGpf0a7B2BHZWBATrBC7E7t
# s3Z52Ao0CW0cgDEf4g5U3eWh++VHEK1kmP9QFi58vwUheuKVQSdpw5OPlcmN2Jsh
# rg1cnPCiroZogwxqLbt2awAdlq3yFnv2FoMkuYjPaqhHMS+a3ONxPdcAfmJH0c6I
# ybgY+g5yjcGjPa8CQGr/aZuW4hCoELQ3UAjWwz0wggYHMIID76ADAgECAgphFmg0
# AAAAAAAcMA0GCSqGSIb3DQEBBQUAMF8xEzARBgoJkiaJk/IsZAEZFgNjb20xGTAX
# BgoJkiaJk/IsZAEZFgltaWNyb3NvZnQxLTArBgNVBAMTJE1pY3Jvc29mdCBSb290
# IENlcnRpZmljYXRlIEF1dGhvcml0eTAeFw0wNzA0MDMxMjUzMDlaFw0yMTA0MDMx
# MzAzMDlaMHcxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xITAf
# BgNVBAMTGE1pY3Jvc29mdCBUaW1lLVN0YW1wIFBDQTCCASIwDQYJKoZIhvcNAQEB
# BQADggEPADCCAQoCggEBAJ+hbLHf20iSKnxrLhnhveLjxZlRI1Ctzt0YTiQP7tGn
# 0UytdDAgEesH1VSVFUmUG0KSrphcMCbaAGvoe73siQcP9w4EmPCJzB/LMySHnfL0
# Zxws/HvniB3q506jocEjU8qN+kXPCdBer9CwQgSi+aZsk2fXKNxGU7CG0OUoRi4n
# rIZPVVIM5AMs+2qQkDBuh/NZMJ36ftaXs+ghl3740hPzCLdTbVK0RZCfSABKR2YR
# JylmqJfk0waBSqL5hKcRRxQJgp+E7VV4/gGaHVAIhQAQMEbtt94jRrvELVSfrx54
# QTF3zJvfO4OToWECtR0Nsfz3m7IBziJLVP/5BcPCIAsCAwEAAaOCAaswggGnMA8G
# A1UdEwEB/wQFMAMBAf8wHQYDVR0OBBYEFCM0+NlSRnAK7UD7dvuzK7DDNbMPMAsG
# A1UdDwQEAwIBhjAQBgkrBgEEAYI3FQEEAwIBADCBmAYDVR0jBIGQMIGNgBQOrIJg
# QFYnl+UlE/wq4QpTlVnkpKFjpGEwXzETMBEGCgmSJomT8ixkARkWA2NvbTEZMBcG
# CgmSJomT8ixkARkWCW1pY3Jvc29mdDEtMCsGA1UEAxMkTWljcm9zb2Z0IFJvb3Qg
# Q2VydGlmaWNhdGUgQXV0aG9yaXR5ghB5rRahSqClrUxzWPQHEy5lMFAGA1UdHwRJ
# MEcwRaBDoEGGP2h0dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1
# Y3RzL21pY3Jvc29mdHJvb3RjZXJ0LmNybDBUBggrBgEFBQcBAQRIMEYwRAYIKwYB
# BQUHMAKGOGh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljcm9z
# b2Z0Um9vdENlcnQuY3J0MBMGA1UdJQQMMAoGCCsGAQUFBwMIMA0GCSqGSIb3DQEB
# BQUAA4ICAQAQl4rDXANENt3ptK132855UU0BsS50cVttDBOrzr57j7gu1BKijG1i
# uFcCy04gE1CZ3XpA4le7r1iaHOEdAYasu3jyi9DsOwHu4r6PCgXIjUji8FMV3U+r
# kuTnjWrVgMHmlPIGL4UD6ZEqJCJw+/b85HiZLg33B+JwvBhOnY5rCnKVuKE5nGct
# xVEO6mJcPxaYiyA/4gcaMvnMMUp2MT0rcgvI6nA9/4UKE9/CCmGO8Ne4F+tOi3/F
# NSteo7/rvH0LQnvUU3Ih7jDKu3hlXFsBFwoUDtLaFJj1PLlmWLMtL+f5hYbMUVbo
# nXCUbKw5TNT2eb+qGHpiKe+imyk0BncaYsk9Hm0fgvALxyy7z0Oz5fnsfbXjpKh0
# NbhOxXEjEiZ2CzxSjHFaRkMUvLOzsE1nyJ9C/4B5IYCeFTBm6EISXhrIniIh0EPp
# K+m79EjMLNTYMoBMJipIJF9a6lbvpt6Znco6b72BJ3QGEe52Ib+bgsEnVLaxaj2J
# oXZhtG6hE6a/qkfwEm/9ijJssv7fUciMI8lmvZ0dhxJkAj0tr1mPuOQh5bWwymO0
# eFQF1EEuUKyUsKV4q7OglnUa2ZKHE3UiLzKoCG6gW4wlv6DvhMoh1useT8ma7kng
# 9wFlb4kLfchpyOZu6qeXzjEp/w7FW1zYTRuh2Povnj8uVRZryROj/TGCBIUwggSB
# AgEBMIGQMHkxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xIzAh
# BgNVBAMTGk1pY3Jvc29mdCBDb2RlIFNpZ25pbmcgUENBAhMzAAAAymzVMhI1xOFV
# AAEAAADKMAkGBSsOAwIaBQCggZ4wGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQw
# HAYKKwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFEUM
# F5prSvgmieyveBSCFo1x/0mtMD4GCisGAQQBgjcCAQwxMDAuoBaAFABXAGUAYgAg
# AEQAZQBwAGwAbwB5oRSAEmh0dHA6Ly93d3cuaWlzLm5ldDANBgkqhkiG9w0BAQEF
# AASCAQCNLG1lbEZQHu3s176MKgFBsJwRbX6KMZcMZWMvsKqAeUhn04+B+OfvFTBn
# LTuKGpNOA20p/RXWqYapM7ujUPomY1ufsTGRHNYQLRTgXeDik3zOGnVWDe00z9Ak
# QSPaWc6IvV1bG1XCxDDCB5E/iuvHpY1XkEqI9CtXScX7gDPEZ07NOYdqAq7YeJ7a
# kqtyYUejlOSLbkyF3fbLZLTcjysjNCH7daUX2Dhxw4wu3VLoUxMBslYCRS25Exmn
# D1ABbtlwiBXEA/3lstMRprYUcCWW+SrnT5C6YmYHfE62DRFvj2dsiNZ3K5j9A/2b
# oOCJIhXs0uMlReZ4Faey8GFgEKDVoYICKDCCAiQGCSqGSIb3DQEJBjGCAhUwggIR
# AgEBMIGOMHcxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYD
# VQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xITAf
# BgNVBAMTGE1pY3Jvc29mdCBUaW1lLVN0YW1wIFBDQQITMwAAADQkMUDJoMF5jQAA
# AAAANDAJBgUrDgMCGgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkq
# hkiG9w0BCQUxDxcNMTQwNTA1MjA0MTMzWjAjBgkqhkiG9w0BCQQxFgQUWutrNyHp
# OyGB6WfyJVznGres1VswDQYJKoZIhvcNAQEFBQAEggEAOJWprWE3uKq26Gv0Bf8n
# tLKvdLlYAiD5bELSgVbK6xPhm0PpQ5knxsSpMpuyliuhkm8kwwdeUO60kuAi/QSF
# QLyOg7cmNjeQ4QQKfjKVVtT57nctOXjO/WkbmNpYyFkXS7s0nR3SVS3CSpyc1LvR
# dCoenhSGgKLFfIKZjuhMconeH58tuvMyC1V4M6GK6UZuto18oUYClLkNacEGt2Pr
# 0rqjugCS6RgibLT/m64ahWXicbTpSgko0C2cuxB1Relo8u+cYS2XJfXfVhlqkT2L
# VL6Wb4SouR8dmyBEVfBKsK930Bgo6jXRNtQksH3eX8nNThVm6mHBJowjj1opfF6L
# 0w==
# SIG # End signature block
