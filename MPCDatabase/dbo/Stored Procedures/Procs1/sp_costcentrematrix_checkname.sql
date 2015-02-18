CREATE PROCEDURE dbo.sp_costcentrematrix_checkname
(@Name varchar(50),
@MatrixID int)
AS
Select Name from tbl_costcentrematrices where Name=@Name and MatrixID<>@MatrixID 
                RETURN