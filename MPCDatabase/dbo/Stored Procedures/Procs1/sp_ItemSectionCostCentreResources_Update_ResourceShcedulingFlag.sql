CREATE PROCEDURE dbo.sp_ItemSectionCostCentreResources_Update_ResourceShcedulingFlag
(
	@ResourceID int,
	@IsScheduleable smallint
)
AS
	Update tbl_section_costcentre_resources set tbl_section_costcentre_resources.IsScheduleable = @IsScheduleable where SectionCostCentreResourceID= @ResourceID
	RETURN