CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_CheckFieldChoiceByID
	(

		@ID int
	
		
	)
    AS

SELECT tbl_customizedfieldsdata.Value 
         FROM tbl_customizedfieldsdata 
         INNER JOIN tbl_customizedfields ON (tbl_customizedfieldsdata.FieldID = tbl_customizedfields.FieldID) 
         WHERE tbl_customizedfields.FieldType = 7 and tbl_customizedfieldsdata.Value = @ID
	RETURN