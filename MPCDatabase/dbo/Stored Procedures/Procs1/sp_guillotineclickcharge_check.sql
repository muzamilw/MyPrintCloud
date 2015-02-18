CREATE PROCEDURE dbo.sp_guillotineclickcharge_check
(@ID int)
AS
select ID from tbl_machine_guillotinecalc where ID=@ID        
        
                 RETURN