CREATE PROCEDURE dbo.sp_WorkFlowPreferences_update
(@ItemTitleMandatory bit,
@OrderTitleMandatory bit,
@ShowTax2AndTax3Information bit,
@Nearest int,
@ShowTaxInformation bit,
@ShippingNoteordertojob bit,
@CopyJobDescriptionToInvoice bit,
@EstimateTitleMandatory bit,
@GoodsOrderForm bit,
@RoundSellingPrice bit,
@ShippingNoteIquirytoorder bit,
@ID int,
@ShowZeroValueCostCentre bit
)
AS
	update tbl_workflowpreferences set OrderTitleMandatory=@OrderTitleMandatory,ItemTitleMandatory=@ItemTitleMandatory,Nearest=@Nearest,ShowTax2AndTax3Information=@ShowTax2AndTax3Information,ShowTaxInformation=@ShowTaxInformation,   ShippingNoteordertojob=@ShippingNoteordertojob,CopyJobDescriptionToInvoice=@CopyJobDescriptionToInvoice, EstimateTitleMandatory=@EstimateTitleMandatory,GoodsOrderForm=@GoodsOrderForm,RoundSellingPrice=@RoundSellingPrice,   ShippingNoteIquirytoorder=@ShippingNoteIquirytoorder,ShowZeroValueCostCentre=@ShowZeroValueCostCentre where ID=@ID
	RETURN