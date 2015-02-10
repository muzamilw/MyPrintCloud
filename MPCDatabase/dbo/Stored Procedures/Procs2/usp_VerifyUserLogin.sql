--exec usp_VerifyUserLogin 'naveed@mpc.com', 'mpc'
CREATE PROCEDURE [dbo].[usp_VerifyUserLogin]
	@Email	varchar(50),
	@Password	varchar(50)
AS
BEGIN
				declare @CompanyID int
				select @CompanyID = ContactCompanyID from tbl_contacts
				where	Email = @Email
				And		Password = @Password 
				
				select  ContactID, AddressID, ContactCompanyID, FirstName, MiddleName, LastName, Title, 
						HomeTel1, HomeTel2, HomeExtension1, HomeExtension2, Mobile, Pager, Email, FAX, 
						JobTitle, Department, DOB, Notes, IsDefaultContact, HomeAddress1, HomeAddress2, 
						HomeCity, HomeStateID, HomePostCode, HomeCountryID, SecretQuestion, SecretAnswer, 
						Password,
						(select ct.TypeName from tbl_contactcompanies cc 
							inner join tbl_contactcompanytypes ct on ct.TypeID = cc.TypeID
							where cc.ContactCompanyID = @CompanyID
						) As CustomerType
						
				from	 tbl_contacts cu
				where	Email = @Email
				And		Password = @Password
END