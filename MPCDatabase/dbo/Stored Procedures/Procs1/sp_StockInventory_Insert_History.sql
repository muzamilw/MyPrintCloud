CREATE PROCEDURE dbo.sp_StockInventory_Insert_History

	(
		@ItemID integer,
		@changeDate Datetime,
		@userID integer,
		@oldPrice float,
		@newPrice float,
		@GrnID integer
--		@parameter2 datatype OUTPUT
	)

AS
	INSERT into tbl_stockitem_history (itemID,ChangeDate,UserID,OldPackPrice,
	NewPackPrice,GRNID) 
	VALUES (@ItemID,@changeDate,@userID,@oldPrice,@newPrice,@GrnID)
	RETURN