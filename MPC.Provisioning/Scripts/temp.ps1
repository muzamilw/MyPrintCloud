

#check if the site exists
if (Test-Path $siteName -pathType container)
{
	if(!(Test-Path -Path $mpcContentFolder )){
		# this command will create virtual folder
		New-Item $virtualDirectoryPath -type VirtualDirectory -physicalPath $mpcContentFolder
	}
	#if(!(Test-Path -Path "$mpcContentFolder\Assets" )){
	#	New-Item "$mpcContentFolder\Assets" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Attachments" )){
	#	New-Item "$mpcContentFolder\Attachments" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Categories" )){
	#	New-Item "$mpcContentFolder\Categories" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\CompanyBanners" )){
	#	New-Item "$mpcContentFolder\CompanyBanners" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\CostCenres" )){
	#	New-Item "$mpcContentFolder\CostCenres" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Designer" )){
	#	New-Item "$mpcContentFolder\Designer" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Media" )){
	#	New-Item "$mpcContentFolder\Media" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Organisations" )){
	#	New-Item "$mpcContentFolder\Organisations" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Products" )){
	#	New-Item "$mpcContentFolder\Products" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\SecondaryPages" )){
	#	New-Item "$mpcContentFolder\SecondaryPages" -type directory
	#}
	#if(!(Test-Path -Path "$mpcContentFolder\Stores" )){
	#	New-Item "$mpcContentFolder\Stores" -type directory
	#}
	
	if(!(Test-Path -Path $resourceFileDirectory ))
	{
		New-Item $resourceFileDirectory -type directory
		if(!(Test-Path -Path $resourceFileDirectoryWithOrganisation )){
		New-Item $resourceFileDirectoryWithOrganisation -type directory
		}
	}
	elseif(!(Test-Path -Path $resourceFileDirectoryWithOrganisation )){
		New-Item $resourceFileDirectoryWithOrganisation -type directory
	}
	
    echo "App already exists"
}else
{
    return "Website Site not created"
}
return "App Created"



#create all other directories
#New-Item "$mpcContentFolder\Artworks" -type directory
#New-Item "$mpcContentFolder\Attachments" -type directory
#New-Item "$mpcContentFolder\Categories" -type directory
#New-Item "$mpcContentFolder\CompanyBanners" -type directory
#New-Item "$mpcContentFolder\CostCenres" -type directory
#New-Item "$mpcContentFolder\Designer" -type directory
#New-Item "$mpcContentFolder\Media" -type directory
#New-Item "$mpcContentFolder\Organisations" -type directory
#New-Item "$mpcContentFolder\Products" -type directory
#New-Item "$mpcContentFolder\SecondaryPages" -type directory
#New-Item "$mpcContentFolder\Stores" -type directory<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>

</body>
</html>
