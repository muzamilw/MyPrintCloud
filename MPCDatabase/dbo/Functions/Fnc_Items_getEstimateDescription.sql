CREATE FUNCTION dbo.Fnc_Items_getEstimateDescription
	(
	
	@ItemID int
	
	
	)
RETURNS Varchar(4000)
AS
	BEGIN
		Declare @EstimateDescription varchar(4000)
		Declare @EstimateTitle1 Varchar(4000)
		Declare @EstimateTitle2 Varchar(4000)
		Declare @EstimateTitle3 Varchar(4000)
		Declare @EstimateTitle4 Varchar(4000)
		Declare @EstimateTitle5 Varchar(4000)
		Declare @EstimateTitle6 Varchar(4000)
		Declare @EstimateTitle7 Varchar(4000)
		Declare @EstimateTitle8 Varchar(4000)
		Declare @EstimateTitle9 Varchar(4000)
		Declare @EstimateTitle10 Varchar(4000)
		
		Declare @EstimateDescription1 varchar(4000)
		Declare @EstimateDescription2 varchar(4000)
		Declare @EstimateDescription3 varchar(4000)
		Declare @EstimateDescription4 varchar(4000)
		Declare @EstimateDescription5 varchar(4000)
		Declare @EstimateDescription6 varchar(4000)
		Declare @EstimateDescription7 varchar(4000)
		Declare @EstimateDescription8 varchar(4000)
		Declare @EstimateDescription9 varchar(4000)
		Declare @EstimateDescription10 varchar(4000)
	
		Declare @JobDescriptionTitle1 varchar(4000)
		Declare @JobDescriptionTitle2 varchar(4000)
		Declare @JobDescriptionTitle3 varchar(4000)
		Declare @JobDescriptionTitle4 varchar(4000)
		Declare @JobDescriptionTitle5 varchar(4000)
		Declare @JobDescriptionTitle6 varchar(4000)
		Declare @JobDescriptionTitle7 varchar(4000)
		Declare @JobDescriptionTitle8 varchar(4000)
		Declare @JobDescriptionTitle9 varchar(4000)
		Declare @JobDescriptionTitle10 varchar(4000)
		
		Declare @JobDescription1 varchar(4000)
		Declare @JobDescription2 varchar(4000)
		Declare @JobDescription3 varchar(4000)
		Declare @JobDescription4 varchar(4000)
		Declare @JobDescription5 varchar(4000)
		Declare @JobDescription6 varchar(4000)
		Declare @JobDescription7 varchar(4000)
		Declare @JobDescription8 varchar(4000)
		Declare @JobDescription9 varchar(4000)
		Declare @JobDescription10 varchar(4000)
		
		Declare @InvoiceDescription varchar(4000)
		Declare @Status int
		Declare Cur cursor for 	SELECT     EstimateDescriptionTitle1, EstimateDescriptionTitle2, EstimateDescriptionTitle3, EstimateDescriptionTitle4, EstimateDescriptionTitle5, 
	                      EstimateDescriptionTitle6, EstimateDescriptionTitle7, EstimateDescriptionTitle8, EstimateDescriptionTitle9, EstimateDescriptionTitle10, 
	                      EstimateDescription1, EstimateDescription2, EstimateDescription3, EstimateDescription4, EstimateDescription5, EstimateDescription6, 
	                      EstimateDescription7, EstimateDescription8, EstimateDescription9, EstimateDescription10, JobDescriptionTitle1, JobDescriptionTitle2, 
	                      JobDescriptionTitle3, JobDescriptionTitle4, JobDescriptionTitle5, JobDescriptionTitle6, JobDescriptionTitle7, JobDescriptionTitle8, 
	                      JobDescriptionTitle9, JobDescriptionTitle10, JobDescription1, JobDescription2, JobDescription3, JobDescription4, JobDescription5, JobDescription6, 
	                      JobDescription7, JobDescription8, JobDescription9, JobDescription10, InvoiceDescription,Status 
					FROM         tbl_items Where ItemID=@ItemID
		
		
		open Cur
		
		
		FETCH NEXT FROM Cur into @EstimateTitle1,@EstimateTitle2,@EstimateTitle3,@EstimateTitle4,@EstimateTitle5,@EstimateTitle6,@EstimateTitle7,@EstimateTitle8,@EstimateTitle9,@EstimateTitle10,
							@EstimateDescription1,@EstimateDescription2,@EstimateDescription3,@EstimateDescription4,@EstimateDescription5,@EstimateDescription6,@EstimateDescription7,@EstimateDescription8,@EstimateDescription9,@EstimateDescription10,
							@JobDescriptionTitle1,@JobDescriptionTitle2,@JobDescriptionTitle3,@JobDescriptionTitle4,@JobDescriptionTitle5,@JobDescriptionTitle6,@JobDescriptionTitle7,@JobDescriptionTitle8,@JobDescriptionTitle9,@JobDescriptionTitle10,
							@JobDescription1,@JobDescription2,@JobDescription3,@JobDescription4,@JobDescription5,@JobDescription6,@JobDescription7,@JobDescription8,@JobDescription9,@JobDescription10,
							@InvoiceDescription,@Status
							
		--When Item is in Estimate or Order
		if @Status=1  or @Status=2
		begin	
			set @EstimateDescription = @EstimateTitle1 + ' : ' + @EstimateDescription1 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle2 + ' : ' + @EstimateDescription2 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle3 + ' : ' + @EstimateDescription3 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle4 + ' : ' + @EstimateDescription4 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle5 + ' : ' + @EstimateDescription5 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle6 + ' : ' + @EstimateDescription6 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle7 + ' : ' + @EstimateDescription7 + char(13)
			set @EstimateDescription = @EstimateDescription + @EstimateTitle8 + ' : ' + @EstimateDescription8 + char(13)
			
			--as We are not using these options
			--set @EstimateDescription = @EstimateDescription + @EstimateTitle9 + ' : ' + @EstimateDescription9 + char(13)
			--set @EstimateDescription = @EstimateDescription + @EstimateTitle10 + ' : ' + @EstimateDescription10 + char(13)
		end
		--When Item is a job
		else if @Status=3
		begin
			set @EstimateDescription = @JobDescriptionTitle1 + ' : ' + @JobDescription1 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle2 + ' : ' + @JobDescription2 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle3 + ' : ' + @JobDescription3 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle4 + ' : ' + @JobDescription4 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle5 + ' : ' + @JobDescription5 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle6 + ' : ' + @JobDescription6 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle7 + ' : ' + @JobDescription7 + char(13)
			set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle8 + ' : ' + @JobDescription8 + char(13)
			--as We are not using these options
			--set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle9 + ' : ' + @JobDescription9 + char(13)
			--set @EstimateDescription = @EstimateDescription + @JobDescriptionTitle10 + ' : ' + @JobDescription10 + char(13)
		
		end 
		-- When Item is in invoice
		else if @Status=4
		begin
			set @EstimateDescription = @InvoiceDescription
		end
		
		close Cur
			
		RETURN @EstimateDescription
		END