CREATE PROCEDURE dbo.sp_Customer_Address_GetCustomerAddressesByCustomerID

	(
		@CustomerID int
	)

AS
	SELECT tbl_customeraddresses.AddressID,
        tbl_customeraddresses.CustomerID,tbl_customeraddresses.AddressName,tbl_customeraddresses.Address1,
        tbl_customeraddresses.Address2,tbl_customeraddresses.Address3,tbl_customeraddresses.City,
        tbl_customeraddresses.StateID,tbl_customeraddresses.CountryID,tbl_customeraddresses.PostCode,
        tbl_customeraddresses.Fax,tbl_customeraddresses.Email,tbl_customeraddresses.URL,tbl_customeraddresses.Tel1,
        tbl_customeraddresses.Tel2,tbl_customeraddresses.Extension1,tbl_customeraddresses.Extension2,
        tbl_customeraddresses.Reference,tbl_customeraddresses.FAO,tbl_customeraddresses.IsDefaultAddress,tbl_customeraddresses.IsDefaultShippingAddress 
        FROM tbl_customeraddresses 
        WHERE CustomerID=@CustomerID
	RETURN