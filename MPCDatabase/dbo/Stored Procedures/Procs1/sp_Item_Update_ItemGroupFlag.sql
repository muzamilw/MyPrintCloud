CREATE PROCEDURE dbo.sp_Item_Update_ItemGroupFlag
(
@IsGroupItem smallint,
@ItemID int
)
AS
	update tbl_items set tbl_items.IsGroupItem = @IsGroupItem where ItemID=@ItemID
RETURN