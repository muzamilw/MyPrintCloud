CREATE PROCEDURE dbo.sp_inkcoverage_get_byID
(@CoverageGroupID int)
                  AS
select CoverageGroupID,GroupName,Percentage from tbl_ink_coverage_groups where CoverageGroupID =@CoverageGroupID
return