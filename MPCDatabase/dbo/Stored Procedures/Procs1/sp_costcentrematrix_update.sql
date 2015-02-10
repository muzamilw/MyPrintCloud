CREATE PROCEDURE dbo.sp_costcentrematrix_update
(
@Name varchar(50),
@Description varchar(255),
@RowsCount int,
@ColumnsCount int,
@MatrixID int)
AS
update tbl_costcentrematrices set [Name]=@Name,Description=@Description,RowsCount=@RowsCount,ColumnsCount=@ColumnsCount where MatrixID=@MatrixID      
                 RETURN