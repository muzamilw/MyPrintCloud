CREATE PROCEDURE [dbo].[sp_WorkFlowPreferences_get_workflow_bySiteID]
(@SystemSiteID int)
AS
	select ID, SystemSiteID, ShowTaxInformation, ShippingNoteordertojob, CopyJobDescriptionToInvoice,
	 EstimateTitleMandatory, GoodsOrderForm, RoundSellingPrice, ShippingNoteIquirytoorder, 
	 ShowTax2AndTax3Information, Nearest, ItemTitleMandatory, OrderTitleMandatory, ShowZeroValueCostCentre 
	 from tbl_workflowpreferences 
	 where SystemSiteID=@SystemSiteID
	RETURN