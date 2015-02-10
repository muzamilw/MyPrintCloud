
Create PROCEDURE [dbo].[sp_markup_check_markupincostcentres]
(@MarkUpID int)
AS
	SELECT tbl_costcentres.CostCentreID FROM tbl_costcentres 
         WHERE ((tbl_costcentres.DefaultVAId = @MarkUpID))
        RETURN