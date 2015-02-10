CREATE PROCEDURE [dbo].[sp_Delivery_Get_ShippingInformationOfEstimate]

	(
		@EstimateId int
	)

AS
	/* SET NOCOUNT ON */
SELECT tbl_shippinginformation.*,tbl_Addresses.Address1 as AddressInfo FROM tbl_shippinginformation
INNER JOIN tbl_items ON (tbl_shippinginformation.ItemID = tbl_items.ItemID)
right Outer JOIN tbl_Addresses on (tbl_Addresses.AddressID=tbl_shippinginformation.AddressID)
WHERE tbl_items.EstimateID = @EstimateId
	
	RETURN