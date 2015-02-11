CREATE PROCEDURE dbo.sp_itemcatalogue_update
(@Header text,
@Title varchar(50),
@Name varchar(50),
@Description text,
@Footer text,
@City varchar(50),
@State int,
@ZipCode varchar(50),
@Address varchar(50),
@Country int,
@Fax varchar(50),
@Mobile varchar(50),
@Tel varchar(50),
@CompanyName varchar(50),
@IsDisabled bit,
@CustomerID int,
@IsCatalogPrivate bit,
@ID int,
@Thumbnail image,
@Image image
)

AS

update tbl_finishedgoods_catalogue set Title=@Title,Name=@Name,Description=@Description,
Footer=@Footer,City=@City,State=@State,ZipCode=@ZipCode,Address=@Address,Country=@Country,
Header=@Header,Fax=@Fax,Mobile=@Mobile,Tel=@Tel,CompanyName=@CompanyName,IsDisabled=@IsDisabled,
CustomerID=@CustomerID,IsCatalogPrivate=@IsCatalogPrivate, Thumbnail=@Thumbnail, Image=@Image
where ID=@ID
                     RETURN