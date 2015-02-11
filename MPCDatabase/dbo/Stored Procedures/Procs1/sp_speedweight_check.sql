CREATE PROCEDURE dbo.sp_speedweight_check
(@ID int)
                  AS
select ID from tbl_machine_speedweightlookup where ID=@ID
             RETURN