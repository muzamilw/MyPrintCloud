CREATE PROCEDURE [dbo].[sp_Customer_Users_isUserExists]
	(
	@CustomerID int,
	@UserName varchar(50)
	
	)
AS
	SELECT tbl_Contacts.FirstName FROM tbl_Contacts 
         WHERE tbl_Contacts.FirstName=@UserName AND tbl_Contacts.ContactCompanyID=@CustomerID
	RETURN