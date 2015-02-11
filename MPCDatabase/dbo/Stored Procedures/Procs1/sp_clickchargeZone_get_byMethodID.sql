CREATE PROCEDURE dbo.sp_clickchargeZone_get_byMethodID
(
         @MethodID int
         )
AS
select * from tbl_machine_clickchargezone where MethodID=@MethodID
        RETURN