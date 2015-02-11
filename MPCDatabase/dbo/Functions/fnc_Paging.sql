CREATE FUNCTION dbo.fnc_Paging
	(
	@Tables varchar(1000),
	@PK varchar(100),
	@JoinStatements varchar(1000)='',
	@Fields varchar(5000) = '*',
	@Filter varchar(5000) = NULL,
	@PageNumber int = 1,
	@PageSize int = 10,
	@Sort varchar(200) = NULL,
	@Group varchar(1000) = NULL
	)
RETURNS @table_variable TABLE (			 PressID int, 
                                         MachineID int, 
                                         MachineName varchar(500), 
                                         GuillotineName varchar(500),
                                         Qty1 int, 
                                         Qty2 int, 
                                         Qty3 int, 
                                         SectionName varchar(500), 
                                         PrintViewLayoutLandScape int, 
                                         PrintViewLayoutPortrait int, 
                                         PrintViewLayout int, 
                                         SectionNotes text, 
                                         jobSelectedQty int, 
                                         Title varchar(500), 
                                         Estimate_Name varchar(500), 
                                         CustomerName varchar(500), 
                                         Order_DeliveryDate datetime, 
                                         TargetPrintDate datetime, 
                                         ArtworkByDate datetime, 
                                         DataByDate datetime, 
                                         TargetBindDate datetime, 
                                         Estimate_Code varchar(500), 
                                         GuillotineID int, 
                                         ItemSectionID int, 
                                         PressSpeed1 float, 
                                         PressSpeed2 float, 
                                         PressSpeed3 float, 
                                         SectionNo varchar(500), 
                                         JobCode varchar(500), 
                                         CustomerID int, 
                                         ItemID int, 
                                         ItemCode varchar(500),  
                                         JobEstimatedStartDateTime datetime, 
                                         JobEstimatedCompletionDateTime datetime, 
                                         JobCreationDateTime  datetime) 
AS
	BEGIN
	
	
	/*Default Sorting*/
IF @Sort IS NULL OR @Sort = ''
	SET @Sort = @PK

/*Find the @PK type*/
DECLARE @SortTable varchar(100)
DECLARE @SortName varchar(100)
DECLARE @strSortColumn varchar(200)
DECLARE @operator char(2)
DECLARE @type varchar(100)
DECLARE @prec int

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
	SET @strGroup = ''




/*Execute dynamic query*/	
/*
EXEC(

'
DECLARE @SortColumn ' + @type + '
PRINT ISNULL(@SortColumn,0)
SET ROWCOUNT ' + @strStartRow + '
print ' + @strStartRow + '
SELECT @SortColumn=' + @strSortColumn + ' FROM ' + @Tables + @JoinStatements+  @strFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort + '
print @SortColumn
SET ROWCOUNT ' + @strPageSize + '
SELECT ' + @Fields + ' FROM ' + @Tables + '' + @JoinStatements +' WHERE ' + @strSortColumn + @operator + ' @SortColumn ' + @strSimpleFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort + ''
)
*/

-- print 'SELECT @SortColumn=' + @strSortColumn + ' FROM ' + @Tables + @JoinStatements+  @strFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort + ''
-- print  'SELECT ' + @Fields + ' FROM ' + @Tables + '' + @JoinStatements +' WHERE ' + @strSortColumn + @operator + ' @SortColumn ' + @strSimpleFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort + ''
	
	
		/* INSERT INTO @table_variable 
				sql select statement  */ 
		/* alternative sql statement or statements */ 
	RETURN
	END