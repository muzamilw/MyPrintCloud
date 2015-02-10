CREATE PROCEDURE [dbo].[sp_Delivery_Get_JobDeliveries]

	(
		@ID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT distinct tbl_deliverynotes.DeliveryNoteID,tbl_deliverynotes.Code,tbl_contactcompanies.Name,tbl_contactcompanies.FlagID,(case tbl_deliverynotes.IsStatus when 1 then 'Un-Delivered' when 2 then 'Delivered' end) as Status
        FROM tbl_deliverynotes
        INNER JOIN tbl_contactcompanies ON (tbl_deliverynotes.contactcompanyid = tbl_contactcompanies.contactcompanyid)
        Inner Join tbl_deliverynote_details on (tbl_deliverynotes.DeliveryNoteID=tbl_deliverynote_details.DeliveryNoteID)
        WHERE tbl_deliverynotes.JobID = @ID or tbl_deliverynote_details.ItemID=@ID
	
	RETURN