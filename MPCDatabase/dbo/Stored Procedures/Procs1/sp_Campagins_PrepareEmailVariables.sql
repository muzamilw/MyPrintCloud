CREATE PROCEDURE dbo.sp_Campagins_PrepareEmailVariables

	(
		@CampaignID int ,
		@Variables Varchar(8000),		
		@EmailCampaignTrakingID int
	)

AS
 

declare @variable varchar(2000)
declare @Pos int

declare @RefTableName varchar(200)
declare @RefFieldName varchar(200)
declare @CriteriaFieldName varchar(200)

declare @EmailFlag smallint --1-Customer 0-Supplier 2-CustomerContact 3-SupplierContact 4 CustomerAddress 5 SupplierAddress
declare @ID int , @ContactID int

declare @Qry varchar(8000)

declare @RowCount int


create table #EmailVariables(variable varchar(2000),value varchar(6000))

SET @Variables = LTRIM(RTRIM(@Variables))+ '|'
SET @Pos = CHARINDEX('|', @Variables, 1)


IF REPLACE(@Variables, '|', '') <> ''
	BEGIN
			WHILE @Pos > 0
		BEGIN
		
		
		
			select @variable = LTRIM(RTRIM(LEFT(@Variables, @Pos - 1)))
		
			
		
								
			
			IF @variable <> ''
			BEGIN
			
			
				
				
				
			select @RefTableName=RefTableName ,@RefFieldName =RefFieldName ,
				@CriteriaFieldName = CriteriaFieldName  from tbl_system_email_variables 
				where VariableTag=@variable
			
			select @EmailFlag =  MailFlag, @ID = ID,@ContactID = ContactID
			from tbl_EmailCampaignTracking where EmailCampaignTrakingID= @EmailCampaignTrakingID
			
	
		
			if @EmailFlag = 2 or @EmailFlag = 3  -- Customer Contact , Supplier Contact
				begin
					
					if @RefTableName = 'tbl_customers' or @RefTableName = 'tbl_suppliers'
						begin
					
							set @Qry=	'insert into #EmailVariables(variable,value)
								select '''+@variable+''' as variable, '+@RefFieldName+' as value from '+@RefTableName+'
								where '+@CriteriaFieldName+'='+convert(varchar(200),@ID)+' '
								
						end
					
					else
						begin
					
							if @RefTableName='tbl_customeraddresses' or @RefTableName='tbl_supplieraddresses' 
								begin
								print 'address'
								set @Qry=	'insert into #EmailVariables(variable,value)
								select top 1 '''+@variable+''' as variable, '+@RefFieldName+' as value from '+@RefTableName+'
								where '+@CriteriaFieldName+'='+convert(varchar(200),@ID)+' '
								end
							else
								begin
								set @Qry=	'insert into #EmailVariables(variable,value)
								select '''+@variable+''' as variable, '+@RefFieldName+' as value from '+@RefTableName+'
								where '+@CriteriaFieldName+'='+convert(varchar(200),@ContactID)+' '
								end	
							
						end
																							
				end 


			if @EmailFlag = 4 or @EmailFlag = 5 -- 4 CustomerAddress 5 SupplierAddress 
				begin 
				
				print 'Customer/Supplier Address '
				
				if @RefTableName = 'tbl_customers' or @RefTableName = 'tbl_suppliers'
						begin
				
							set @Qry=	'insert into #EmailVariables(variable,value)
								select '''+@variable+''' as variable, '+@RefFieldName+' as value from '+@RefTableName+'
								where '+@CriteriaFieldName+'='+convert(varchar(200),@ID)+' '
								
						end
				
				else
				
					begin
				
				
				
					set @Qry=	'insert into #EmailVariables(variable,value)
								select '''+@variable+''' as variable, '+@RefFieldName+' as value from '+@RefTableName+'
								where '+@CriteriaFieldName+'='+convert(varchar(200),@ContactID)+' '
								
					end 
											
				end 

			print @Qry

			exec(@Qry)
			
			set @rowCount =0
			
			if @rowcount = isnull((select 	@@RowCount  from #EmailVariables where variable= @variable),0)
				insert into #EmailVariables(variable,value) values(@variable,'')
			
			
			
			END
			
			
			SET @Variables = RIGHT(@Variables, LEN(@Variables) - @Pos)
			SET @Pos = CHARINDEX('|', @Variables, 1)
						

		END
	END	
	

select * from #EmailVariables

drop table #EmailVariables

RETURN