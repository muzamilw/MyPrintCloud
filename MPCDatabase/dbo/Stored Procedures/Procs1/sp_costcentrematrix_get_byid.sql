CREATE PROCEDURE dbo.sp_costcentrematrix_get_byid
(
@MatrixID int)
AS
Select * from tbl_costcentrematrices where MatrixID=@MatrixID
                RETURN