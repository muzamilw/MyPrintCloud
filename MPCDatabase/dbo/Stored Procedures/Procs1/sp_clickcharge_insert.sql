CREATE PROCEDURE dbo.sp_clickcharge_insert
(@MethodID int,
@Sheetcost float,
@SheetPrice float,
@Sheets int,
@TimePerHour float)
AS
	insert into tbl_machine_clickchargelookup (MethodID,Sheetcost,SheetPrice,Sheets,TimePerHour) VALUES (@MethodID,@Sheetcost,@SheetPrice,@Sheets,@TimePerHour);
	Select @@Identity as LookUpId
	RETURN