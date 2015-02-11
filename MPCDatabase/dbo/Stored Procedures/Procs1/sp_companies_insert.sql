CREATE PROCEDURE dbo.sp_companies_insert
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
@Language varchar(50))
                  AS
insert into tbl_company (CompanyName,Address1,Address2,Address3,City,State,Country,ZipCode,Tel,Fax,Mobile,Email,Url,RegNo,TaxNo,TaxName,Language) values (@CompanyName,@Address1,@Address2,@Address3,@City,@State,@Country,@ZipCode,@Tel,@Fax,@Mobile,@Email,@Url,@RegNo,@TaxNo,@TaxName,@Language);
Select @@Identity

return