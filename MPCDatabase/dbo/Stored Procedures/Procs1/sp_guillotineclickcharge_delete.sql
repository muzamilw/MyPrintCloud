CREATE PROCEDURE dbo.sp_guillotineclickcharge_delete
(@ID int)
AS
delete from tbl_machine_guillotinecalc where ID=@ID
        
        
                 RETURN