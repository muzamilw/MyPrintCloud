CREATE PROCEDURE dbo.sp_perhour_update
(@MethodID int,
@SpeedCost float,
@SpeedPrice float,
@Speed int,
@ID int)
AS
update tbl_machine_perhourlookup set MethodID=@MethodID,SpeedCost=@SpeedCost,SpeedPrice=@SpeedPrice,Speed=@Speed where ID=@ID
                 RETURN