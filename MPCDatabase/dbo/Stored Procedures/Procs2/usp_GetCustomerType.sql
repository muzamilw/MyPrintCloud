CREATE PROCEDURE [dbo].[usp_GetCustomerType]
	@CustomerID	int
	
AS
BEGIN
		select	ct.TypeName as CustomerType
		from	tbl_contactcompanies cc
		inner join tbl_contactcompanytypes ct on ct.TypeID = cc.TypeID
		where	cc.ContactCompanyID = @CustomerID
END