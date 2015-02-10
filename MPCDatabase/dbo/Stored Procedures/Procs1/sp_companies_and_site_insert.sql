CREATE PROCEDURE dbo.sp_companies_and_site_insert
(@CompanyName varchar(50) ,
@Address1 varchar(255) ,
@Address2 varchar(255) ,
@Address3 varchar(255) ,
@City varchar(50) ,
@State int ,
@Country int ,
@ZipCode varchar(50) ,
@Tel varchar(50) ,
@Fax varchar(50),
@Mobile varchar(50),
@Email varchar(50),
@Url varchar(50),
@RegNo varchar(50),
@TaxNo varchar(50),
@TaxName varchar(50),
@Language varchar(50),

/* SITE PARAMETERS */

@IsDefaultSite bit,
@SiteName varchar(50),
@SiteAddress1 varchar(255),
@SiteAddress2 varchar(255),
@SiteAddress3 varchar(255),
@SiteCity varchar(50),
@SiteState int,
@SiteCountry int,
@SiteZipCode varchar(50),
@SiteTel varchar(50),
@SiteFax varchar(50),
@SiteMobile varchar(50),
@SiteEmail varchar(50),
@SiteUrl varchar(50),
@CompanyID int,
@StateTaxID int,

/* end here */

@OldSite int,
@IsProductCopy bit,
@IsCostCenterGroups bit,
@IsCustomerCopy bit,
@IsSupplierCopy bit,
@IsCostCenterCopy bit,
@IsPaginationCopy bit,
@IsMachineCopy bit,
@IsInventoryCopy bit,
@IsCopyAccount bit,
@OldCompanyID int,
@IsCostCentreCaegoryCopy bit,
@IsFinishGoodCopy bit
)
                  AS
                  
Declare @SiteID int
		                                                    
insert into tbl_company (CompanyName,Address1,Address2,Address3,City,State,Country,ZipCode,Tel,Fax,Mobile,Email,Url,RegNo,TaxNo,TaxName,Language) values (@CompanyName,@Address1,@Address2,@Address3,@City,@State,@Country,@ZipCode,@Tel,@Fax,@Mobile,@Email,@Url,@RegNo,@TaxNo,@TaxName,@Language);
Select @CompanyID=@@Identity


/*return @CompanyID*/

/* SITE */

execute sp_copy_compulsory_tables_CopmanyLevel @OldCompanyID, @CompanyID

			
execute @SiteID=sp_companies_site_insert @IsDefaultSite,@SiteName,@SiteAddress1,@SiteAddress2,@SiteAddress3,@SiteCity,@SiteState,@SiteCountry,@SiteZipCode,@SiteTel,@SiteFax,@SiteMobile,@SiteEmail,@SiteUrl,@CompanyID,@StateTaxID,@OldSite,@IsProductCopy,@IsCostCenterGroups,@IsCustomerCopy,@IsSupplierCopy,@IsCostCenterCopy,@IsPaginationCopy,@IsMachineCopy,@IsInventoryCopy,@IsCopyAccount,@OldCompanyID,@IsCostCentreCaegoryCopy,@IsFinishGoodCopy
		
		
			
Select @SiteID
return