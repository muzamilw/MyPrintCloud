CREATE PROCEDURE dbo.sp_Customer_CustomizedFields_UpdateCustomerFields
	(
		@ID int,
		@Value varchar(255)
		
	)
AS

UPDATE  tbl_customizedfieldsdata
        SET Value=@Value 
        WHERE ID = @ID
	RETURN