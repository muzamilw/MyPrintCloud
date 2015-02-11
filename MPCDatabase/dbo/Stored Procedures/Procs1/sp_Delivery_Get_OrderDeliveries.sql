CREATE PROCEDURE [dbo].[sp_Delivery_Get_OrderDeliveries]

	(
		@ID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT  tbl_deliverynotes.DeliveryNoteID,tbl_deliverynotes.Code,tbl_ContactCompanies.Name,tbl_ContactCompanies.FlagID,
	(case tbl_deliverynotes.IsStatus when 1 then 'Un-Delivered' when 2 then 'Delivered' when 0 then 'Cancelled' end) as Status
        FROM tbl_deliverynotes
        INNER JOIN tbl_ContactCompanies ON (tbl_deliverynotes.Contactcompanyid = tbl_ContactCompanies.ContactCompanyID)
        WHERE tbl_deliverynotes.EstimateID = @ID
	
	RETURN