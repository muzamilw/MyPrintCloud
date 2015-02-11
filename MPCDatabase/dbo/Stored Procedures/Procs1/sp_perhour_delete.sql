CREATE PROCEDURE dbo.sp_perhour_delete
(
@ID int
)
AS
delete from tbl_machine_perhourlookup where ID=@ID
             RETURN