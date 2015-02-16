CREATE PROCEDURE dbo.sp_guillotineclickcharge_get_PTV
(@ID int)
AS
select * from tbl_machine_guilotine_ptv where GuilotineID=@ID     
        
                 RETURN