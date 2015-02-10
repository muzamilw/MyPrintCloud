CREATE PROCEDURE dbo.sp_meterperhour_delete
(@ID int)
                  AS
delete from tbl_machine_meterperhourlookup where ID=@ID
             RETURN