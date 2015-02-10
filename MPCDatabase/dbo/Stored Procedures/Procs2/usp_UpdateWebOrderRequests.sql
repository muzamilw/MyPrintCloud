-- =============================================
-- Author:		Kashif Shahzad
-- Create date: 02/09/2011
-- Description:	Update/Insert RFQ
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdateWebOrderRequests]
	@RequestID			numeric = 0,
	@CustomerID			numeric = 0,
	@RequestTitle		varchar(100) = NULL,
	@RequestType		varchar(100) = NULL,
	@RequestStatus		varchar(50) = NULL,
	@FinsihedProductSize varchar(100) = NULL,
	@FinishedProductUnit varchar(50) = NULL,
	@Quantity1			numeric = NULL,
	@Quantity2			numeric = NULL,
	@Quantity3			numeric = NULL,
	@IsCrease			bit = NULL,
	@IsFold				bit = NULL,
	@IsDieCut			bit = NULL,
	@AdditionalInformation varchar(max) = NULL,
	@DelieveryInstruction varchar(max) = NULL,
	@OrderDate			datetime = NULL,
	@DeliveryDate		datetime = NULL,
	@QuoteStatus		varchar(50) = NULL,
	@SubTotal			numeric = NULL,
	@VAT				numeric = NULL,
	@CreateID			numeric = NULL,
	@ActiveInd			bit,	
	@RFQ1Path			varchar(2000) = NULL,
	@RFQ2Path			varchar(2000) = NULL,
	@RFQ3Path			varchar(2000) = NULL,
	@RFQ4Path			varchar(2000) = NULL,
	@RFQ1Height		int = NULL,
	@RFQ2Height		int = NULL,
	@RFQ3Height		int = NULL,
	@RFQ4Height		int = NULL,
	@RFQ1Width			int = NULL,
	@RFQ2Width			int = NULL,
	@RFQ3Width			int = NULL,
	@RFQ4Width			int = NULL,
	@OutputID	numeric output

AS
BEGIN
	SET NOCOUNT ON;

	IF (@RequestID = 0)
		BEGIN
			INSERT INTO WebOrderRequests
					(CustomerID, RequestTitle, RequestType, RequestStatus, FinsihedProductSize,
					FinishedProductUnit, Quantity1, Quantity2, Quantity3, IsCrease, IsFold,
					IsDieCut, AdditionalInformation, DelieveryInstruction, OrderDate,
					DeliveryDate, QuoteStatus, SubTotal, VAT,
					RFQ1Path, RFQ2Path, RFQ3Path, RFQ4Path,
					RFQ1Height, RFQ2Height, RFQ3Height, RFQ4Height,
					RFQ1Width, RFQ2Width, RFQ3Width, RFQ4Width,
					CreateID, CreateDate, ActiveInd)
				VALUES (@CustomerID, @RequestTitle, @RequestType, @RequestStatus, @FinsihedProductSize,
					@FinishedProductUnit, @Quantity1, @Quantity2, @Quantity3, @IsCrease, @IsFold,
					@IsDieCut, @AdditionalInformation, @DelieveryInstruction, @OrderDate,
					@DeliveryDate, @QuoteStatus, @SubTotal, @VAT,
					@RFQ1Path,@RFQ2Path,@RFQ3Path,@RFQ4Path,
					@RFQ1Height,@RFQ2Height,@RFQ3Height,@RFQ4Height,
					@RFQ1Width,@RFQ2Width,@RFQ3Width,@RFQ4Width,
					@CreateID, getdate(), @ActiveInd)
			SELECT @OutputID = scope_identity()
		END
		ELSE
		BEGIN
			UPDATE WebOrderRequests
				SET CustomerID = @CustomerID,
					RequestTitle = @RequestTitle,
					RequestType = @RequestType,
					RequestStatus = @RequestStatus,
					FinsihedProductSize = @FinsihedProductSize,
					FinishedProductUnit = @FinishedProductUnit,
					Quantity1 = @Quantity1,
					Quantity2 = @Quantity2,
					Quantity3 = @Quantity3,
					IsCrease = @IsCrease,
					IsFold = @IsFold,
					IsDieCut = @IsDieCut,
					AdditionalInformation = @AdditionalInformation,
					DelieveryInstruction = @DelieveryInstruction,
					OrderDate = @OrderDate,
					DeliveryDate = @DeliveryDate,
					QuoteStatus = @QuoteStatus,
					SubTotal = @SubTotal,
					VAT = @VAT,
					ModifyID = @CreateID,
					ModifyDate = getDate(),
					ActiveIND = @ActiveInd,	
					RFQ1Path = @RFQ1Path,
					RFQ2Path = @RFQ2Path,
					RFQ3Path = @RFQ3Path,
					RFQ4Path = @RFQ4Path,
					RFQ1Height = @RFQ1Height,
					RFQ2Height = @RFQ2Height,
					RFQ3Height = @RFQ3Height,
					RFQ4Height = @RFQ4Height,
					RFQ1Width = @RFQ1Width,
					RFQ2Width = @RFQ2Width,
					RFQ3Width = @RFQ3Width,
					RFQ4Width = @RFQ4Width		
			WHERE RequestID = @RequestID

			SELECT @OutputID = @RequestID
		END
			
--	update webOrderRequestDetail		
--	 where RequestID = @OutputID
	
END

/****** Object:  StoredProcedure [dbo].[usp_ShoppingCart]    Script Date: 05/24/2011 09:32:15 ******/
SET ANSI_NULLS ON