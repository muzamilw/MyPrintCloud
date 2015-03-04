﻿

CREATE PROCEDURE [dbo].[proc_ProductsUpdate]
(
	@ProductID int,
	@OfficeID int = NULL,
	@ProductName nvarchar(255) = NULL,
	@ProductCode nvarchar(50) = NULL,
	@Description text = NULL,
	@PrintSpecification text = NULL,
	@CategoryID int = NULL,
	@PDFTemplate text = NULL,
	@LowResPDFTemplates text = NULL,
	@PrePrintPDFTemplates text = NULL,
	@BackgroundArtwork text = NULL,
	@Side2PDFTemplate text = NULL,
	@Side2LowResPDFTemplates text = NULL,
	@Side2PrePrintPDFTemplates text = NULL,
	@Side2BackgroundArtwork text = NULL,
	@Thumbnail text = NULL,
	@Image text = NULL,
	@IsProductEditable bit = NULL,
	@IsDisabled bit = NULL,
	@IsStockItem bit = NULL,
	@StockQuantity int = NULL,
	@UPrice float = NULL,
	@ReOrderLavel int = NULL,
	@ReOrderQty int = NULL,
	@AllocatedQty int = NULL,
	@QtyPerBox int = NULL,
	@IsFromBottomToTop bit = NULL,
	@DiplayOrder int = NULL,
	@IsApproveBeforeOrder bit = NULL,
	@CountryCode nvarchar(100) = NULL,
	@ProductTypeID int = NULL,
	@CostCentre varchar(255) = NULL,
	@IsTemplate bit = NULL,
	@ParentId int = NULL,
	@UserId int = NULL,
	@PTempId int = NULL,
	@Type int = NULL,
	@ApplyVAT bit = NULL,
	@ApplyShippingCharges bit = NULL,
	@DepartmentID int = NULL,
	@IsUserSaveProduct bit = NULL,
	@IsAllDept bit = NULL,
	@IsPrePrint bit = NULL,
	@IsDoubleSide bit = NULL,
	@IsUsePDFFile bit = NULL,
	@PDFTemplateWidth float = NULL,
	@PDFTemplateHeight float = NULL,
	@IsUseBackGroundColor bit = NULL,
	@BgR int = NULL,
	@BgG int = NULL,
	@BgB int = NULL,
	@IsUseSide2BackGroundColor bit = NULL,
	@Side2BgR int = NULL,
	@Side2BgG int = NULL,
	@Side2BgB int = NULL,
	@CuttingMargin float = NULL,
	@IsPopularProduct bit = NULL,
	@PaperSizeId int = NULL,
	@IsMultiPage bit = NULL,
	@TotelPage int = NULL,
	@ProfileId int = NULL,
	@IsVoucherApplied bit = NULL,
	@Orientation int = NULL,
	@DefaultPageTempId int = NULL,
	@MinimumPages int = NULL,
	@IsNotUseDesigner bit = NULL,
	@IsImportData bit = NULL,
	@TaxID int = NULL,
	@IsRequiredArtwork bit = NULL,
	@ArtworkMaxWidth float = NULL,
	@ArtworkMaxHeight float = NULL,
	@ArtworkMaxSize float = NULL
)
AS
BEGIN

	SET NOCOUNT OFF
	DECLARE @Err int

	UPDATE [Products]
	SET
		[OfficeID] = @OfficeID,
		[ProductName] = @ProductName,
		[ProductCode] = @ProductCode,
		[Description] = @Description,
		[PrintSpecification] = @PrintSpecification,
		[CategoryID] = @CategoryID,
		[PDFTemplate] = @PDFTemplate,
		[LowResPDFTemplates] = @LowResPDFTemplates,
		[PrePrintPDFTemplates] = @PrePrintPDFTemplates,
		[BackgroundArtwork] = @BackgroundArtwork,
		[Side2PDFTemplate] = @Side2PDFTemplate,
		[Side2LowResPDFTemplates] = @Side2LowResPDFTemplates,
		[Side2PrePrintPDFTemplates] = @Side2PrePrintPDFTemplates,
		[Side2BackgroundArtwork] = @Side2BackgroundArtwork,
		[Thumbnail] = @Thumbnail,
		[Image] = @Image,
		[IsProductEditable] = @IsProductEditable,
		[IsDisabled] = @IsDisabled,
		[IsStockItem] = @IsStockItem,
		[StockQuantity] = @StockQuantity,
		[UPrice] = @UPrice,
		[ReOrderLavel] = @ReOrderLavel,
		[ReOrderQty] = @ReOrderQty,
		[AllocatedQty] = @AllocatedQty,
		[QtyPerBox] = @QtyPerBox,
		[IsFromBottomToTop] = @IsFromBottomToTop,
		[DiplayOrder] = @DiplayOrder,
		[IsApproveBeforeOrder] = @IsApproveBeforeOrder,
		[CountryCode] = @CountryCode,
		[ProductTypeID] = @ProductTypeID,
		[CostCentre] = @CostCentre,
		[IsTemplate] = @IsTemplate,
		[ParentId] = @ParentId,
		[UserId] = @UserId,
		[PTempId] = @PTempId,
		[Type] = @Type,
		[ApplyVAT] = @ApplyVAT,
		[ApplyShippingCharges] = @ApplyShippingCharges,
		[DepartmentID] = @DepartmentID,
		[IsUserSaveProduct] = @IsUserSaveProduct,
		[IsAllDept] = @IsAllDept,
		[IsPrePrint] = @IsPrePrint,
		[IsDoubleSide] = @IsDoubleSide,
		[IsUsePDFFile] = @IsUsePDFFile,
		[PDFTemplateWidth] = @PDFTemplateWidth,
		[PDFTemplateHeight] = @PDFTemplateHeight,
		[IsUseBackGroundColor] = @IsUseBackGroundColor,
		[BgR] = @BgR,
		[BgG] = @BgG,
		[BgB] = @BgB,
		[IsUseSide2BackGroundColor] = @IsUseSide2BackGroundColor,
		[Side2BgR] = @Side2BgR,
		[Side2BgG] = @Side2BgG,
		[Side2BgB] = @Side2BgB,
		[CuttingMargin] = @CuttingMargin,
		[IsPopularProduct] = @IsPopularProduct,
		[PaperSizeId] = @PaperSizeId,
		[IsMultiPage] = @IsMultiPage,
		[TotelPage] = @TotelPage,
		[ProfileId] = @ProfileId,
		[IsVoucherApplied] = @IsVoucherApplied,
		[Orientation] = @Orientation,
		[DefaultPageTempId] = @DefaultPageTempId,
		[MinimumPages] = @MinimumPages,
		[IsNotUseDesigner] = @IsNotUseDesigner,
		[IsImportData] = @IsImportData,
		TaxID = @TaxID,
		IsRequiredArtwork = @IsRequiredArtwork,
		ArtworkMaxWidth = @ArtworkMaxWidth,
		ArtworkMaxHeight = @ArtworkMaxHeight,
		ArtworkMaxSize = @ArtworkMaxSize

	WHERE
		[ProductID] = @ProductID


	SET @Err = @@Error


	RETURN @Err
END