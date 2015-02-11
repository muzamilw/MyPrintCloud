CREATE PROCEDURE [dbo].[sp_PagingTable]

	(
	
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
	
		
	)

AS




DECLARE @strPageSize varchar(50)
DECLARE @strStartRow varchar(50)


SET @strPageSize = CAST(@PageSize AS varchar(50))
SET @strStartRow = CAST(((@PageNumber - 1)*@PageSize + 1) AS varchar(50))


--set @PK =' tbl_Items.ItemID '

create table #PageTable (PID  bigint primary key IDENTITY (1, 1) , UID int)
create table #PageIndex (UID int)

/*
CREATE UNIQUE CLUSTERED
  INDEX [PK_tbl_PageTable] ON #PageTable (PID)
*/
CREATE  
  INDEX [PK_tbl_PageIndex] ON #PageIndex (UID)


--'SELECT ' + @Fields + ' FROM ' + @Tables + '' + @JoinStatements +' WHERE ' + @strSortColumn + @operator + ' @SortColumn ' + @strSimpleFilter + ' ' + @strGroup + ' ORDER BY ' + @Sort +  ' DESC '
exec ('

set rowcount 0

	insert into #pageTable(UID) 
	 SELECT  ' + @PK + ' FROM ' + @Tables + ' ' + @JoinStatements +' WHERE ' +  @Filter + ' ' + @Group + ' ORDER BY ' + @Sort +  ' 


DECLARE @SortColumn int

SET ROWCOUNT '+  @strStartRow +'

select  @SortColumn=PID from #PageTable --option (keep plan)

print @SortColumn

SET ROWCOUNT '+  @strPageSize +'

insert into #pageIndex
select UID from #PageTable where PID >= @SortColumn -- option (keep plan)


          
SELECT ' + @Fields + ' FROM ' + @Tables + ' ' + @JoinStatements +' WHERE ' +  @Filter + ' and  '+ @PK + ' in (Select UID from #pageIndex)' + @Group + ' ORDER BY ' + @Sort +  ' '

 )



select @TotalRec=count(*) from  #pageTable 


---------//////////////////////Print QRY

print 'set rowcount 0

	insert into #pageTable(UID) 
	 select '+@PK+' from Tbl_Items

select * from  #pageTable
DECLARE @SortColumn int

SET ROWCOUNT '+  @strStartRow +'
select  @SortColumn=PID from #PageTable

print @SortColumn

set ROWCOUNT '+@strPageSize+'

insert into #pageIndex
select UID from #PageTable where PID > @SortColumn

SELECT ' + @PK + ' FROM ' + @Tables + '' + @JoinStatements +' WHERE ' +  @Filter + ' and  '+ @PK + ' in (Select UID from #pageIndex)' + @Group + ' ORDER BY ' + @Sort +  ' DESC '


drop table #PageTable 
drop table #PageIndex

--print 'Page Size: ' +@strPageSize
--print 'Start Row: ' + @strStartRow
--print 'page number: '+ CAST(@pageNumber AS varchar(50))
--print 'pk: ' + @PK
	
	
RETURN