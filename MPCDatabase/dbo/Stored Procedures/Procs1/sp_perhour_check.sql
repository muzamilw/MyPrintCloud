CREATE PROCEDURE dbo.sp_perhour_check
(
@ID int
)
AS
select ID from tbl_machine_perhourlookup where ID=@ID
             RETURN