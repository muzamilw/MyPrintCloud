CREATE PROCEDURE [dbo].[usp_CreateCustomer]
		@CustomerName varchar(100),
		@EmailSubcription bit,
		@NewletterSubscription bit
		
AS
BEGIN

		insert into tbl_contactcompanies
			(Name, TypeID, Status, IsCustomer, IsEmailSubscription, IsMailSubscription, CreationDate, IsGeneral)
		select @CustomerName, TypeID, 1, 0, @EmailSubcription, @NewletterSubscription,  GETDATE(), 1 from tbl_contactcompanytypes
		where TypeName = 'Prospect'
		
		select SCOPE_IDENTITY()
		
END