CREATE PROCEDURE dbo.sp_perhour_insert
(@MethodID int,
@SpeedCost float,
@SpeedPrice float,
@Speed int)
AS
insert into tbl_machine_perhourlookup (MethodID,SpeedCost,SpeedPrice,Speed) VALUES (@MethodID,@SpeedCost,@SpeedPrice,@Speed);
Select @@Identity as LookUpId
                 RETURN