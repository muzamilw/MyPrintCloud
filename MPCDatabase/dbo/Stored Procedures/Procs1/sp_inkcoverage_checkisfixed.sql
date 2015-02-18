CREATE PROCEDURE dbo.sp_inkcoverage_checkisfixed
(@InkID int)
                  AS
SELECT tbl_ink_coverage_groups.CoverageGroupID FROM tbl_ink_coverage_groups WHERE  ((tbl_ink_coverage_groups.CoverageGroupID = @InkID)  and (tbl_ink_coverage_groups.IsFixed = 1))
return