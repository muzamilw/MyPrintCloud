CREATE PROCEDURE [dbo].[sp_Customer_CustomerTypes_GetCustomerTypes]

	
AS

SELECT TypeID,TypeName,IsFixed FROM tbl_contactcompanytypes order by TypeName asc

	RETURN