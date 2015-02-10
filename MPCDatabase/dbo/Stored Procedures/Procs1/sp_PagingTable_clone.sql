CREATE PROCEDURE dbo.sp_PagingTable_clone

	(
	
	@Tables varchar(1000),
	@PK varchar(200),
	@JoinStatements varchar(5000)='',
	@Fields varchar(5000) = '*',
	@Filter varchar(5000) = NULL,
	@Sort varchar(200) = NULL,
	@PageNumber int = 1,
	@PageSize int = 10,
	@TotalRec int =0 Output,
	@Group varchar(1000) = NULL
	
		
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


--'select 'SELECT ' + @Fields + ' FROM ' + @Tables + '' + @JoinStatements +' WHERE ' + @Sort + ' @SortColumn ' + @Filter + ' ' + @Group + ' ORDER BY ' + @Sort +  ' DESC '
exec ('

set rowcount 0

	insert into #pageTable(UID) 
	 SELECT ' + @PK + ' FROM ' + @Tables + ' ' + @JoinStatements +' WHERE ' +  @Filter + ' ' + @Group + ' ORDER BY ' + @Sort +  ' 


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


	
RETURN