CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_UpdateData
	(
		@CompanyID int
	)


AS
	SELECT tbl_customizedfields.FieldID,tbl_customizedfields.FieldName,tbl_customizedfields.FieldType,tbl_customizedfields.CompanyID 
	
        FROM tbl_customizedfields where CompanyID=@CompanyID order by tbl_customizedfields.FieldType
	RETURN