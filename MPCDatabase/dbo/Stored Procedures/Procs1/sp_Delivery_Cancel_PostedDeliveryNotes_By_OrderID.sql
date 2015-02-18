
Create PROCEDURE [dbo].[sp_Delivery_Cancel_PostedDeliveryNotes_By_OrderID]

	(
		@EstimateId int
	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_deliverynotes set isstatus=0
	where EstimateId = @EstimateId and isStatus = 2
	
	RETURN