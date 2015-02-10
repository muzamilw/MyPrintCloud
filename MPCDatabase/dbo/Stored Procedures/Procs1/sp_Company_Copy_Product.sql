CREATE PROCEDURE dbo.sp_Company_Copy_Product

	(
		@OldSiteID int,
		@NewSiteID int
	)

AS

	Declare @OldProductID int
		Declare @NewProductID int
		
		Declare ProductCursor Cursor for Select ID from tbl_profile where SystemSiteID=@OldSiteID
		
		open ProductCursor 
		
		FETCH NEXT From ProductCursor into @OldProductID 
		
		While @@FETCH_STATUS = 0 
		BEGIN
			
			--New Product
			insert into tbl_profile  SELECT  TypeID, Name, PressID, IsPressrestrictionApplied, GuilotineID, IsFirstTrim, IsSecondTrim, IsPaperSupplied, IsWholePack, WorkingSheetSizeID, 
                      WorkingSheetSizeIsCustom, WorkingSheetSizeHeight, WorkingSheetSizeWidth, ItemSheetSizeID, ItemSheetSizeIsCustom, ItemSheetSizeHeight, 
                      ItemSheetSizeWidth, IsItemGutterApplied, ItemGutterHorizontal, ItemGutterVertical, IsDoubleSided, IsWorknTurn, IsWorknTunble, IsMultipleQty, 
                      IsRunonQty, RunonQty, Qty1, Qty2, Qty3, BookletPagesPerSection, BookletNoofSections, BookletPages, Side1Inks, Side2Inks, IsInkPrompted, 
                      Description, PadLeafQty, NCRTopPaperID, NCRBottomPaperID, NCRMiddlePaperID, NCRTotalParts, LockedBy, DepartmentID, FlagID, @NewSiteID, 
                      QtyPrefix, IsFilmSupplied, IsPlateSupplied
					  FROM         tbl_profile where ID=@OldProductID
	
			Select @NewProductID=@@Identity
			
			insert into #Products values (@OldProductID,@NewProductID)
			
			--New Product description labels
			insert into tbl_profile_description_labels  SELECT     Title, ValueID, @NewProductID
			FROM         tbl_profile_description_labels where ProfileID=@OldProductID
			
			FETCH NEXT From ProductCursor into @OldProductID 
		END
		
		Close ProductCursor 
		Deallocate ProductCursor 
RETURN