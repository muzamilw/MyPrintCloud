CREATE PROCEDURE dbo.sp_clickchargeZone_delete
(
         @ID int
         )
AS
delete from tbl_machine_clickchargezone where ID=@ID

 RETURN