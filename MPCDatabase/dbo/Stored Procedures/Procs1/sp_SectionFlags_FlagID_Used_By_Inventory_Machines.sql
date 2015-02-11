CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_Machines

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT MachineID from tbl_machines WHERE FlagID=@flagID
	RETURN