CREATE PROCEDURE dbo.sp_companies_update
(@CompanyName varchar(50),
@Address1 varchar(255),
@Address2 varchar(255),
@Address3 varchar(255),
@City varchar(50),
@State int,
@Country int,
@ZipCode varchar(50),
@Tel varchar(50),
@Fax varchar(50),
@Mobile varchar(50),
@Email varchar(50),
@Url varchar(50),
@RegNo varchar(50),
@TaxNo varchar(50),
@TaxName varchar(50),
@CompanyID int)
                  AS
update tbl_company set CompanyName=@CompanyName,Address1=@Address1,Address2=@Address2,Address3=@Address3,City=@City,State=@State,Country=@Country,ZipCode=@ZipCode,Tel=@Tel,Fax=@Fax,Mobile=@Mobile,Email=@Email,URL=@URL,RegNo=@RegNo,TaxNo=@TaxNo,TaxName=@TaxName where CompanyID=@CompanyID



return