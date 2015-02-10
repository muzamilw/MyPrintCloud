CREATE PROCEDURE dbo.sp_PagingOrderBy

(

@Tables varchar(1000),
@PK varchar(100),
@JoinStatements varchar(1000)='',
@Fields varchar(5000) = '*',
@Filter varchar(5000) = NULL,
@Sort varchar(200) = NULL,
@OrderBy varchar(200)=NULL,
@PageNumber int = 1,
@PageSize int = 10,
@TotalRec int =0 Output,
@Group varchar(1000) = NULL
)
AS
/*Default Sorting*/
--IF @Sort IS NULL OR @Sort = ''
--	SET @Sort = @PK
/*Find the @PK type*/
DECLARE @SortTable varchar(100)
DECLARE @SortName varchar(100)
DECLARE @strSortColumn varchar(200)
DECLARE @operator char(2)
DECLARE @type varchar(100)
DECLARE @prec int
declare  @GropIndex varchar(100)
Declare @SortColIndex Varchar(100)



/*Set sorting variables.*/	
IF CHARINDEX('DESC',@Sort)>0
	BEGIN
		SET @strSortColumn = REPLACE(@Sort, 'DESC', '')
		SET @operator = '<='
	END
ELSE
	BEGIN
		IF CHARINDEX('ASC', @Sort) = 0
			SET @strSortColumn = REPLACE(@Sort, 'ASC', '')
		SET @operator = '>='
	END
IF CHARINDEX('.', @strSortColumn) > 0
	BEGIN
		SET @SortTable = SUBSTRING(@strSortColumn, 0, CHARINDEX('.',@strSortColumn))
		SET @SortName = SUBSTRING(@strSortColumn, CHARINDEX('.',@strSortColumn) + 1, LEN(@strSortColumn))
	END
ELSE
	BEGIN
		SET @SortTable = @Tables 
		SET @SortName = @strSortColumn
	END



IF CHARINDEX('DESC',@OrderBy)>0
	BEGIN
		SET @GropIndex = REPLACE( @OrderBy,'DESC',' ')
		SET @operator = '<='
	END

ELSE
	BEGIN
		IF CHARINDEX('ASC', @OrderBy) = 0
			SET @strSortColumn = REPLACE(@OrderBy, 'ASC', ' ')
		SET @operator = '>='
	END




IF CHARINDEX('DESC',@Sort)>0
	BEGIN
		SET  @SortColIndex = REPLACE( @Sort,'DESC',' ')
		
	END







SELECT @type=t.name, @prec=c.prec
FROM sysobjects o 
JOIN syscolumns c on o.id=c.id
JOIN systypes t on c.xusertype=t.xusertype
WHERE o.name = @SortTable AND c.name = @SortName

IF CHARINDEX('char', @type) > 0
   SET @type = @type + '(' + CAST(@prec AS varchar) + ')'
DECLARE @strPageSize varchar(50)
DECLARE @strStartRow varchar(50)
DECLARE @strFilter varchar(1000)
DECLARE @strSimpleFilter varchar(1000)
DECLARE @strGroup varchar(1000)

/*Default Page Number*/
IF @PageNumber < 1
	SET @PageNumber = 1
/*Set paging variables.*/
SET @strPageSize = CAST(@PageSize AS varchar(50))
SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS varchar(50))
/*Set filter & group variables.*/
IF @Filter IS NOT NULL AND @Filter != ''
	BEGIN
		SET @strFilter = ' WHERE ' + @Filter + ' '
		SET @strSimpleFilter = ' AND ' + @Filter + ' '
	END
ELSE
	BEGIN
		SET @strSimpleFilter = ''
		SET @strFilter = ''
	END
IF @Group IS NOT NULL AND @Group != ''
	SET @strGroup = ' GROUP BY ' + @Group + ' '
ELSE
	SET @strGroup = ' '


CREATE TABLE #TRec (RecCount int)


EXEC(
'
DECLARE @SortColumn varchar(200)

insert into #TRec(RecCount)
SELECT  count(*)  FROM ' + @Tables +' '+ @JoinStatements+ @strFilter  + ' ' + @strGroup + '

SET ROWCOUNT ' + @strStartRow + '
print ' + @strStartRow + '
SELECT @SortColumn= Convert(varchar(200),' + @SortColIndex + ')  FROM'+ ' ' + @Tables +'   '+ @JoinStatements+ @strFilter + ' ' + @strGroup + ' ' +@OrderBy +'
print ''Sortco: ''+ convert(varchar(200),@SortColumn)
SET ROWCOUNT ' + @strPageSize + '
SELECT ' + @Fields + '  FROM  ' + @Tables + '  ' + @JoinStatements +'  WHERE  ' + @strSortColumn + ' '+ @operator + '  @SortColumn  ' + @strSimpleFilter + '  ' + @strGroup + '  ' + @OrderBy

)

select @TotalRec = RecCount from #TRec


print @TotalRec 

drop table #TRec

print 'SELECT @SortColumn= Convert(varchar(200),' + @SortColIndex + ')  FROM'+ ' ' + @Tables +'   '+ @JoinStatements+ @strFilter + ' ' + @strGroup + ' ' +@GropIndex 
print  'SELECT ' + @Fields + '  FROM  ' + @Tables + '  ' + @JoinStatements +'  WHERE  ' + @strSortColumn + ' '+ @operator + '  @SortColumn  ' + @strSimpleFilter + '  ' + @strGroup + '  ' + @OrderBy

return