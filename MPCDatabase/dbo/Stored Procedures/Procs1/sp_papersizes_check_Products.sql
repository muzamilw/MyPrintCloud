CREATE PROCEDURE dbo.sp_papersizes_check_Products
(
@PaperSizeID int)
AS
	SELECT tbl_profile.ID fROM tbl_profile WHERE ((tbl_profile.WorkingSheetSizeID = @PaperSizeID)  or (tbl_profile.ItemSheetSizeID = @PaperSizeID))
		RETURN