CREATE PROCEDURE dbo.sp_clickcharge_update
(@MethodID int,
@Sheetcost float,
@SheetPrice float,
@Sheets int,
@ID int,
@TimePerHour float)
AS
update tbl_machine_clickchargelookup set Sheetcost=@Sheetcost,SheetPrice=@SheetPrice,Sheets=@Sheets,MethodID=@MethodID,TimePerHour=@TimePerHour where ID=@ID
	RETURN