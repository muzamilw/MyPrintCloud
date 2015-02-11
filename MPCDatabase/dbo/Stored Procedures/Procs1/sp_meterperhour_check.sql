CREATE PROCEDURE dbo.sp_meterperhour_check
(@ID int)
                  AS
select ID from tbl_machine_meterperhourlookup where ID=@ID
             RETURN