CREATE PROCEDURE dbo.sp_perhour_get_byID
(
@MethodID int
)
AS
select ID,MethodID,SpeedCost,SpeedPrice,Speed from tbl_machine_perhourlookup where MethodID=@MethodID 
             RETURN