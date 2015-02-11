CREATE PROCEDURE dbo.sp_WorkFlowPreferences_add
(@ItemTitleMandatory bit,
@OrderTitleMandatory bit,
@SystemSiteID int,
@ShowTax2AndTax3Information bit,
@Nearest int,
@ShowTaxInformation bit,
@ShippingNoteordertojob bit,
@CopyJobDescriptionToInvoice bit,
@EstimateTitleMandatory bit,
@GoodsOrderForm bit,
@RoundSellingPrice bit,
@ShippingNoteIquirytoorder bit,
@ShowZeroValueCostCentre bit)
AS
	insert into tbl_workflowpreferences (ItemTitleMandatory,OrderTitleMandatory,SystemSiteID,ShowTax2AndTax3Information,Nearest,ShowTaxInformation,ShippingNoteordertojob,CopyJobDescriptionToInvoice,EstimateTitleMandatory,GoodsOrderForm,RoundSellingPrice,ShippingNoteIquirytoorder,ShowZeroValueCostCentre)
	 VALUES (@ItemTitleMandatory,@OrderTitleMandatory,@SystemSiteID,@ShowTax2AndTax3Information,@Nearest,@ShowTaxInformation,@ShippingNoteordertojob,@CopyJobDescriptionToInvoice,@EstimateTitleMandatory,@GoodsOrderForm,@RoundSellingPrice,@ShippingNoteIquirytoorder,@ShowZeroValueCostCentre)
	RETURN