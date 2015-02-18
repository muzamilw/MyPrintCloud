CREATE PROCEDURE dbo.sp_costcentrematrix_delete
(
@MatrixID int)
AS
delete from tbl_costcentrematrices where MatrixID=@MatrixID;
delete from tbl_costcentrematrixdetails where MatrixID=@MatrixID
                RETURN