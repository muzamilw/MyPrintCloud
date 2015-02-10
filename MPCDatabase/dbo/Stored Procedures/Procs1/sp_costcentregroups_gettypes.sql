CREATE PROCEDURE dbo.sp_costcentregroups_gettypes

                  AS
select * from tbl_costcentretypes where IsSystem=0
return