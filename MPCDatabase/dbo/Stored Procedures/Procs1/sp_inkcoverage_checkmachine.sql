CREATE PROCEDURE dbo.sp_inkcoverage_checkmachine
(@InkID int)
                  AS
SELECT tbl_machine_ink_coverage.ID FROM tbl_machine_ink_coverage WHERE ((tbl_machine_ink_coverage.SideInkOrderCoverage = @InkID))
RETURN