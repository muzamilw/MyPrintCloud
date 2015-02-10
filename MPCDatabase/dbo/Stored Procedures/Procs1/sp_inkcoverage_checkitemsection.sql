CREATE PROCEDURE dbo.sp_inkcoverage_checkitemsection
(@InkID int)
                  AS
SELECT tbl_section_inkcoverage.ID FROM tbl_section_inkcoverage WHERE  ((tbl_section_inkcoverage.CoverageGroupID = @InkID))RETURN