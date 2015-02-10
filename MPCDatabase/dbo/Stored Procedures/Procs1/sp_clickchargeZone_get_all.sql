CREATE PROCEDURE dbo.sp_clickchargeZone_get_all

AS
SELECT tbl_lookup_methods.MethodID,tbl_lookup_methods.Name 
         FROM tbl_lookup_methods 
         INNER JOIN tbl_machine_clickchargezone ON (tbl_lookup_methods.MethodID = tbl_machine_clickchargezone.MethodID)
        RETURN