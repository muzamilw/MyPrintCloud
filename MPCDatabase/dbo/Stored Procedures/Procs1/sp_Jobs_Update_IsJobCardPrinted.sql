CREATE PROCEDURE dbo.sp_Jobs_Update_IsJobCardPrinted
@ItemID int , @IsJobCardPrinted bit
AS
	update tbl_items set IsJobCardPrinted=@IsJobCardPrinted where ItemID=@ItemID
	RETURN