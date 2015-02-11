CREATE PROCEDURE dbo.sp_copy_products
(@OldSite int,
@SiteID int)
                  AS
		
		--Adding MarkUp To New Site
		insert into tbl_profile  SELECT  TypeID, Name, PressID, IsPressrestrictionApplied, GuilotineID, IsFirstTrim, IsSecondTrim, IsPaperSupplied, IsWholePack, WorkingSheetSizeID, 
                      WorkingSheetSizeIsCustom, WorkingSheetSizeHeight, WorkingSheetSizeWidth, ItemSheetSizeID, ItemSheetSizeIsCustom, ItemSheetSizeHeight, 
                      ItemSheetSizeWidth, IsItemGutterApplied, ItemGutterHorizontal, ItemGutterVertical, IsDoubleSided, IsWorknTurn, IsWorknTunble, IsMultipleQty, 
                      IsRunonQty, RunonQty, Qty1, Qty2, Qty3, BookletPagesPerSection, BookletNoofSections, BookletPages, Side1Inks, Side2Inks, IsInkPrompted, 
                      Description, PadLeafQty, NCRTopPaperID, NCRBottomPaperID, NCRMiddlePaperID, NCRTotalParts, LockedBy, DepartmentID, FlagID, @SiteID, 
                      QtyPrefix, IsFilmSupplied, IsPlateSupplied
FROM         tbl_profile where SystemSiteID=@OldSite
return