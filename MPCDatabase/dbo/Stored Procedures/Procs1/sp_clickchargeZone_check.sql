CREATE PROCEDURE dbo.sp_clickchargeZone_check
(
         @ID int
         )
AS
select ID from tbl_machine_clickchargezone where ID=@ID
 RETURN