CREATE PROCEDURE [dbo].[usp_GetUserByID]
	@UserID	varchar(50)
	
AS
BEGIN
		declare @AddressID int
		select @AddressID = AddressID from tbl_contacts where ContactID = @UserID 
		
		select  ContactID, isnull(AddressID,0) As AddressID, ContactCompanyID, FirstName, MiddleName, LastName, Title, 
				HomeTel1, HomeTel2, HomeExtension1, HomeExtension2, Mobile, Pager, Email, FAX, 
				JobTitle, Department, DOB, Notes, IsDefaultContact, HomeAddress1, HomeAddress2, 
				HomeCity, HomeStateID, HomePostCode, HomeCountryID, SecretQuestion, SecretAnswer, 
				Password, URL, ISNULL(IsEmailSubscription,0) As EmailSubscription, isnull(IsNewsLetterSubscription,0) As NewsLetterSubscription
		from tbl_contacts
		where ContactID = @UserID
		
		select * from tbl_addresses where AddressID = @AddressID
		
		
END