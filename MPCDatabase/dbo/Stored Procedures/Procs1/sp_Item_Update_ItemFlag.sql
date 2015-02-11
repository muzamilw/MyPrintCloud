CREATE PROCEDURE dbo.sp_Item_Update_ItemFlag
(
@FlagID int,
@ItemID int
)
AS
	update tbl_items set FlagID=@FlagID where ItemID=@ItemID
RETURN