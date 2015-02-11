CREATE PROCEDURE dbo.sp_costcentregroups_getbytype
(@TypeID int)
                  AS
select * from tbl_costcentres where Type=@TypeID
return