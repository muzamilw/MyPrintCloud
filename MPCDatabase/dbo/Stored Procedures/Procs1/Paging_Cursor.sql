CREATE PROCEDURE dbo.Paging_Cursor (
@Tables varchar(1000),
@PK varchar(100),
@Sort varchar(200) = NULL,
@PageNumber int = 1,
@PageSize int = 10,
@Fields varchar(1000) = '*',
@Filter varchar(1000) = NULL,
@Group varchar(1000) = NULL)
AS

/*Find the @PK type*/
DECLARE @PKTable varchar(100)
DECLARE @PKName varchar(100)
DECLARE @type varchar(100)
DECLARE @prec int

IF CHARINDEX('.', @PK) > 0
	BEGIN
		SET @PKTable = SUBSTRING(@PK, 0, CHARINDEX('.',@PK))
		SET @PKName = SUBSTRING(@PK, CHARINDEX('.',@PK) + 1, LEN(@PK))
	END
ELSE
	BEGIN
		SET @PKTable = @Tables
		SET @PKName = @PK
	END

SELECT @type=t.name, @prec=c.prec
FROM sysobjects o 
JOIN syscolumns c on o.id=c.id
JOIN systypes t on c.xusertype=t.xusertype
WHERE o.name = @PKTable AND c.name = @PKName

IF CHARINDEX('char', @type) > 0
   SET @type = @type + '(' + CAST(@prec AS varchar) + ')'

DECLARE @strPageSize varchar(50)
DECLARE @strStartRow varchar(50)
DECLARE @strFilter varchar(1000)
DECLARE @strGroup varchar(1000)

/*Default Sorting*/
IF @Sort IS NULL OR @Sort = ''
	SET @Sort = @PK

/*Default Page Number*/
IF @PageNumber < 1
	SET @PageNumber = 1

/*Set paging variables.*/
SET @strPageSize = CAST(@PageSize AS varchar(50))
SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS varchar(50))

/*Set filter & group variables.*/
IF @Filter IS NOT NULL AND @Filter != ''
	SET @strFilter = ' WHERE ' + @Filter + ' '
ELSE
	SET @strFilter = ''
IF @Group IS NOT NULL AND @Group != ''
	SET @strGroup = ' GROUP BY ' + @Group + ' '
ELSE
	SET @strGroup = ''
	
/*Execute dynamic query*/	





EXEC(
'DECLARE @PageSize int
SET @PageSize = ' + @strPageSize + '

DECLARE @PK ' + @type + '
DECLARE @tblPK TABLE (
            PK  ' + @type + ' NOT NULL PRIMARY KEY
            )

DECLARE PagingCursor CURSOR DYNAMIC READ_ONLY FOR
SELECT '  + @PK + ' FROM ' + @Tables + @strFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort + '

OPEN PagingCursor
FETCH RELATIVE ' + @strStartRow + ' FROM PagingCursor INTO @PK

SET NOCOUNT ON

WHILE @PageSize > 0 AND @@FETCH_STATUS = 0
BEGIN
            INSERT @tblPK (PK)  VALUES (@PK)
            FETCH NEXT FROM PagingCursor INTO @PK
            SET @PageSize = @PageSize - 1
END

CLOSE       PagingCursor
DEALLOCATE  PagingCursor

SELECT ' + @Fields + ' FROM ' + @Tables + ' JOIN @tblPK tblPK ON ' + @PK + ' = tblPK.PK ' + @strFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort
)