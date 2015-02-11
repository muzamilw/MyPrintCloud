CREATE PROCEDURE dbo.sp_StockInventory_Get_Check_Machines_Item
	
	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_machines.MachineID fROM tbl_machines
    WHERE (((tbl_machines.DefaultFilmid = @ItemID) or 
    (tbl_machines.DefaultPlateid = @ItemID)) or 
    ((tbl_machines.DefaultPaperID = @ItemID)))
	RETURN