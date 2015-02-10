
CREATE PROCEDURE MyTest 
	@Tables varchar(1000),
@PK varchar(100),
@JoinStatements varchar(1000)='',
@Fields varchar(8000) = '*',
@Filter varchar(8000) = NULL,
@Sort varchar(200) = NULL,
@PageNumber int = 1,
@PageSize int = 10,
@TotalRec int =0 Output,
@Group varchar(3000) = NULL
AS
BEGIN
	
declare @t varchar(max)
set @t = ' SELECT  @Fields FROM  @Tables @JoinStatements  WHERE 1=1  @strSimpleFilter ORDER BY  @Sort '
exec @t
END