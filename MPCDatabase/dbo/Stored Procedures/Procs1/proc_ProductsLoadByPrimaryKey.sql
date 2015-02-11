
CREATE PROCEDURE [dbo].[proc_ProductsLoadByPrimaryKey]
(
	@ProductID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[ProductID],
		[OfficeID],
		[ProductName],
		[ProductCode],
		[Description],
		[PrintSpecification],
		[CategoryID],
		[PDFTemplate],
		[LowResPDFTemplates],
		[PrePrintPDFTemplates],
		[BackgroundArtwork],
		[Side2PDFTemplate],
		[Side2LowResPDFTemplates],
		[Side2PrePrintPDFTemplates],
		[Side2BackgroundArtwork],
		[Thumbnail],
		[Image],
		[IsProductEditable],
		[IsDisabled],
		[IsStockItem],
		[StockQuantity],
		[UPrice],
		[ReOrderLavel],
		[ReOrderQty],
		[AllocatedQty],
		[QtyPerBox],
		[IsFromBottomToTop],
		[DiplayOrder],
		[IsApproveBeforeOrder],
		[CountryCode],
		[ProductTypeID],
		[CostCentre],
		[IsTemplate],
		[ParentId],
		[UserId],
		[PTempId],
		[Type],
		[ApplyVAT],
		[ApplyShippingCharges],
		[DepartmentID],
		[IsUserSaveProduct],
		[IsAllDept],
		[IsPrePrint],
		[IsDoubleSide],
		[IsUsePDFFile],
		[PDFTemplateWidth],
		[PDFTemplateHeight],
		[IsUseBackGroundColor],
		[BgR],
		[BgG],
		[BgB],
		[IsUseSide2BackGroundColor],
		[Side2BgR],
		[Side2BgG],
		[Side2BgB],
		[CuttingMargin],
		[IsPopularProduct],
		[PaperSizeId],
		[IsMultiPage],
		[TotelPage],
		[ProfileId],
		[IsVoucherApplied],
		[Orientation],
		[DefaultPageTempId],
		[MinimumPages],
		[IsNotUseDesigner],
		[IsImportData],
		TaxID,
		IsRequiredArtwork,
		ArtworkMaxWidth,
		ArtworkMaxHeight,
		ArtworkMaxSize
	FROM [Products]
	WHERE
		([ProductID] = @ProductID)

	SET @Err = @@Error

	RETURN @Err
END