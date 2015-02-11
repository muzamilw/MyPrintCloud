CREATE PROCEDURE dbo.sp_costcentre_get_costcentreresources
(@CostCentreID int)
AS
select * from tbl_costcentre_resources where costcentreid=@CostCentreID
	RETURN