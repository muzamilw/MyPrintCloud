CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomizeFieldByID
	(
		@FieldID int
	)
AS
	select FieldName,FieldType from tbl_customizedfields where FieldID=@FieldID

	RETURN