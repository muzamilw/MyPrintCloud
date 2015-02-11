CREATE PROCEDURE dbo.sp_products_costcentregroups_bysequence
(@Sequence int,
@ProductID int)
AS
	SELECT tbl_costcentre_groups.GroupName, tbl_costcentre_groups.GroupID FROM tbl_costcentre_groups 
	INNER JOIN tbl_profile_costcentre_groups ON (tbl_costcentre_groups.GroupID) = tbl_profile_costcentre_groups.CostCentreGroupID 
	WHERE tbl_profile_costcentre_groups.ProfileID = @ProductID and  tbl_costcentre_groups.Sequence = @Sequence
	RETURN