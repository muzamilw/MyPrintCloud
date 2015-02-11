CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_GetCustomizedFieldsValuesTable
	(
		@FieldID int
	)

AS
	SELECT tbl_customizedfieldsvalues.ID,tbl_customizedfieldsvalues.FieldID,tbl_customizedfieldsvalues.Value
      FROM tbl_customizedfieldsvalues where FieldID=@FieldID
	RETURN