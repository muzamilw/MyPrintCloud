CREATE PROCEDURE dbo.sp_markup_check_markupinsectioncostcentre
(@MarkUpID int)
AS
	SELECT tbl_section_costcentres.SectionCostcentreID 
         FROM tbl_section_costcentres WHERE 
         (((tbl_section_costcentres.Qty1MarkUpID = @MarkUpID) or (tbl_section_costcentres.Qty2MarkUpID = @MarkUpID)) or (tbl_section_costcentres.Qty3MarkUpID = @MarkUpID))
        RETURN