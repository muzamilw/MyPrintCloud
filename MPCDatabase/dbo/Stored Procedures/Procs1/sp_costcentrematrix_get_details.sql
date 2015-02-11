CREATE PROCEDURE dbo.sp_costcentrematrix_get_details
(
@MatrixID int)
AS
select * from tbl_costcentrematrixdetails where MatrixID=@MatrixID order by Id
                RETURN