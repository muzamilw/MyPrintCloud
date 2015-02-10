



CREATE PROCEDURE [dbo].[sp_companies_site_update]
(@IsDefaultSite bit,
@SiteName varchar(50),
@Address1 varchar(255),
@Address2 varchar(255),
@Address3 varchar(255),
@City varchar(50),
@State varchar(100),
@Country varchar(100),
@ZipCode varchar(50),
@Tel varchar(50),
@Fax varchar(50),
@Mobile varchar(50),
@Email varchar(50),
@Url varchar(50),
@CompanyID int,
@StateTaxID int,
@SiteID int)
                  AS
update tbl_company_sites set CompanySiteName=@SiteName,Address1=@Address1,Address2=@Address2,Address3=@Address3,City=@City,State=@State,Country=@Country,ZipCode=@ZipCode,Tel=@Tel,Fax=@Fax,Mobile=@Mobile,Email=@Email,URL=@URL,CompanyID=@CompanyID,StateTaxID=@StateTaxID where CompanySiteID=@SiteID
return