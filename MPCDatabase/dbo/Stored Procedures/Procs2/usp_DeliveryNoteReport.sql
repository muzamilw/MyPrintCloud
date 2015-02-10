
CREATE procedure [dbo].[usp_DeliveryNoteReport]
(
	@DeliveryNoteID int
)
As
select	  cc.Name As CompanyName,
		  sup.Name As SupplierName, sup.HomeContact,
		  ISNULL(c.FirstName, '') + ' ' + ISNULL(c.LastName, '') As CotactFullName, 
		  ad.AddressName, ad.Address1,ad.City,ad.State as StateName, ad.PostCode,ad.Tel1,ad.Country As CountryName,
		 dd.Description, dd.ItemQty, dd.GrossItemTotal, 
          dn.Code,dn.DeliveryDate,dn.OrderReff,dn.CustomerOrderReff,dn.CsNo,
          CASE 
			  WHEN dn.IsStatus = 1 THEN 'Un Delivered'
			  WHEN dn.IsStatus = 2 THEN 'Delivered' 
		 END As DeliveryStatus
		
from	tbl_deliverynotes dn
		inner join tbl_contactcompanies cc on cc.ContactCompanyID = dn.ContactCompanyID
		inner join tbl_contactcompanies sup on sup.ContactCompanyID = dn.SupplierID
		inner join tbl_contacts c on c.contactID = dn.ContactId
		inner join tbl_addresses ad on ad.AddressID = dn.AddressID
		left join tbl_deliverynote_details dd on dd.DeliveryNoteID = dn.DeliveryNoteID
where	dn.DeliveryNoteID = @DeliveryNoteID