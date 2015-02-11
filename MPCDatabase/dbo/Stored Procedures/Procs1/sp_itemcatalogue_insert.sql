CREATE PROCEDURE dbo.sp_itemcatalogue_insert
(@Header text,
@Title varchar(50),
@Name varchar(50),
@Description text,
@Footer text,
@City varchar,
@State int,
@ZipCode varchar(50),
@Address varchar(50),
@Country int,
@Fax varchar(50),
@Mobile varchar(50),
@Tel varchar(50),
@SystemSiteID int,
@CompanyName varchar(50),
@IsDisabled bit,
@CustomerID int,
@IsCatalogPrivate bit,
@Thumbnail image,
@Image image
)
                  AS
insert into tbl_finishedgoods_catalogue (Header,Title,Name,Description,Footer,City,State,ZipCode,
Address,Country,Fax,Mobile,Tel,SystemSiteID,CompanyName,IsDisabled,CustomerID,IsCatalogPrivate, Thumbnail,Image)
 VALUES 
 (@Header,@Title,@Name,@Description,@Footer,@City,@State,@ZipCode,
 @Address,@Country,@Fax,@Mobile,@Tel,@SystemSiteID,@CompanyName,@IsDisabled,@CustomerID,@IsCatalogPrivate,@Thumbnail,@Image);
Select @@Identity as CatalogueID
                     RETURN