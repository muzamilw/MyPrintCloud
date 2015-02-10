CREATE PROCEDURE dbo.sp_costcentrematrix_insert
(@SystemSiteID int,
@Name varchar(50),
@Description varchar(255),
@RowsCount int,
@ColumnsCount int)
AS
insert into tbl_costcentrematrices (SystemSiteID,Name,Description,RowsCount,ColumnsCount) VALUES (@SystemSiteID,@Name,@Description,@RowsCount,@ColumnsCount);
SELECT @@IDENTITY AS MatrixID      
                 RETURN