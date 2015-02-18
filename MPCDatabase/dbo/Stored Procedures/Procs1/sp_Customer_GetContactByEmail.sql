
/****** Object:  StoredProcedure [dbo].[sp_Customer_GetDefaultContact]    Script Date: 03/05/2014 14:47:14 ******/

CREATE PROCEDURE [dbo].[sp_Customer_GetContactByEmail]

	(
		@Email varchar(100),
		@CustomerContactID Int
	)
AS

	if @CustomerContactID = 0 
	BEGIN
		SELECT * FROM  tbl_contacts WHERE Email = @Email
	END
	
	if @CustomerContactID <> 0  
	BEGIN
		SELECT * FROM  tbl_contacts WHERE Email = @Email and ContactID <> @CustomerContactID
	END
	
	RETURN