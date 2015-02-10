CREATE PROCEDURE dbo.sp_copy_customizefields
(@OldCompany int,
@CompanyID int)
                  AS
		insert into tbl_customizedfields  SELECT     FieldName, FieldType, @CompanyID
		FROM         tbl_customizedfields where CompanyID=@OldCompany
		
		
return