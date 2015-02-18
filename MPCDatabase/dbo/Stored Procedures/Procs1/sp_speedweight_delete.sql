CREATE PROCEDURE dbo.sp_speedweight_delete
(@ID int)
                  AS
delete from tbl_machine_speedweightlookup where ID=@ID
             RETURN