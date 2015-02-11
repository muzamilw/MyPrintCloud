CREATE PROCEDURE dbo.sp_Correspondence_getCorrespondenceData

(
	@ReferenceID int =0,
	@ReferenceType smallint =0

)	
AS
	
	
	declare @RefTableName varchar(100),
			@RefFieldName varchar(100),
			@RefKeyID int,
			@CorrespondenceID int ,
			@date datetime ,
			@Title varchar(50),
			@Direction varchar(5),
			@Address varchar(100),
			@Type int,
			@Sendby varchar(100),
			@SendTo varchar(100),
			@QryStr varchar(8000),
			@Reference varchar(100)
			
	

		create table #TMP_Correspondence (CorrespondenceID int , [date] datetime , Title varchar(50),Direction varchar(5),
										Address varchar(100),Type int,Sendby varchar(100),SendTo varchar(100),Reference varchar(100))

if @ReferenceID =0 

	begin
	declare Correspondence_Cursor  Cursor FAST_FORWARD
		for 
			select c.RefTableName,c.RefFieldName,c.RefKeyID,c.CorrespondenceID,c.[Date],c.Title,case c.Direction
			when 0 then 'IN' 
			Else 'OUT'
			end  Direction,Address,Type,u.FullName Sentby ,NULL,c.Reference
			from tbl_Correspondence c,tbl_systemusers u
			where c.UID = u.SystemUserID 	
	end 
else 

	begin
		declare Correspondence_Cursor  Cursor FAST_FORWARD
		for 
			select c.RefTableName,c.RefFieldName,c.RefKeyID,c.CorrespondenceID,c.[Date],c.Title,case c.Direction
			when 0 then 'IN' 
			Else 'OUT'
			end  Direction,Address,Type,u.FullName Sentby ,NULL,c.Reference
			from tbl_Correspondence c,tbl_systemusers u
			where c.UID = u.SystemUserID  and c.ReferenceID =@ReferenceID and ReferenceType=@ReferenceType
	
	end

	open Correspondence_Cursor

	fetch Correspondence_Cursor 
		into
		@RefTableName,@RefFieldName,@RefKeyID,@CorrespondenceID,@date,@Title,@Direction,@Address,@Type,@Sendby,@SendTo,@Reference
		


	while @@FETCH_STATUS <> -1
		begin
		
		
		
	--Customer/Supplier Contacts
	if @RefTableName ='tbl_suppliercontacts' or @RefTableName ='tbl_customercontacts'
		begin
		set @QryStr =	'
		declare @SendTo1 varchar(100)
		select @SendTo1 = FirstName +'' ''+LastName from '+@RefTableName +' where '+@RefFieldName+'='+convert(varchar(20),@RefKeyID)+'
		
		  insert into #TMP_Correspondence 
							values('+convert(varchar(20),@CorrespondenceID) +','''+convert(varchar(50), @date)+''' ,'''+ @Title +''' ,'''+@Direction +''',
										'''+@Address+''' ,'+convert(varchar(20),@Type)+' ,'''+@Sendby +''',@SendTo1,'''+@Reference+'''  )'
	
		end
	
	--Customer Address
	if @RefTableName ='tbl_customeraddresses'
		begin
		set @QryStr =	'
		declare @SendTo1 varchar(100)
		select @SendTo1 = c.CustomerName from '+@RefTableName +' r,tbl_customers c
			where '+@RefFieldName+'='+convert(varchar(20),@RefKeyID)+' and c.CustomerID = r.CustomerID
			  insert into #TMP_Correspondence 
							values('+convert(varchar(20),@CorrespondenceID) +','''+convert(varchar(50), @date)+''' ,'''+ @Title +''' ,'''+@Direction +''',
										'''+@Address+''' ,'+convert(varchar(20),@Type)+' ,'''+@Sendby +''',@SendTo1,'''+@Reference+'''  )'
			
		
		end 	
		
		--Supplier Address
	if @RefTableName ='tbl_supplieraddresses'
		begin
		set @QryStr =	'
		declare @SendTo1 varchar(100)
		select @SendTo1 = s.SupplierName from '+@RefTableName +' r,tbl_suppliers s
			where '+@RefFieldName+'='+convert(varchar(20),@RefKeyID)+' and s.SupplierID= r.SupplierID
				  insert into #TMP_Correspondence 
							values('+convert(varchar(20),@CorrespondenceID) +','''+convert(varchar(50), @date)+''' ,'''+ @Title +''' ,'''+@Direction +''',
										'''+@Address+''' ,'+convert(varchar(20),@Type)+' ,'''+@Sendby +''',@SendTo1 ,'''+@Reference+''' )'
		end 	
		--- Supplier
	if @RefTableName ='tbl_suppliers'
		begin
		set @QryStr =	'
		declare @SendTo1 varchar(100)
		select @SendTo1 = r.SupplierName from '+@RefTableName +' r
			where '+@RefFieldName+'='+convert(varchar(20),@RefKeyID)+'	
				  insert into #TMP_Correspondence 
							values('+convert(varchar(20),@CorrespondenceID) +','''+convert(varchar(50), @date)+''' ,'''+ @Title +''' ,'''+@Direction +''',
										'''+@Address+''' ,'+convert(varchar(20),@Type)+' ,'''+@Sendby +''',@SendTo1 ,'''+@Reference+''' )'
		end 					
		 
		 -- Customer
	if @RefTableName ='tbl_customers'
		begin
		set @QryStr =	'
		declare @SendTo1 varchar(100)
		select @SendTo1 = r.CustomerName  from '+@RefTableName +' r
			where '+@RefFieldName+'='+convert(varchar(20),@RefKeyID)+'
				  insert into #TMP_Correspondence 
							values('+convert(varchar(20),@CorrespondenceID) +','''+convert(varchar(50), @date)+''' ,'''+ @Title +''' ,'''+@Direction +''',
										'''+@Address+''' ,'+convert(varchar(20),@Type)+' ,'''+@Sendby +''',@SendTo1 ,'''+@Reference+''' )'
		end 
		 

		print @QryStr
		exec (@QryStr)
		
		
		fetch Correspondence_Cursor 
		into
		@RefTableName,@RefFieldName,@RefKeyID,@CorrespondenceID,@date,@Title,@Direction,@Address,@Type,@Sendby,@SendTo,@Reference
		
		end
	

select * from #TMP_Correspondence order by [date] desc


drop table #TMP_Correspondence

close Correspondence_Cursor
deallocate Correspondence_Cursor
	
	RETURN