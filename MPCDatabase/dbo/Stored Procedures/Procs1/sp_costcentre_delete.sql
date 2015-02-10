
Create PROCEDURE [dbo].[sp_costcentre_delete]
(@ID int)

AS
	Declare @Result int
	Declare @SubResult int
	Set @Result = 1
	Set @SubResult = 0
	
	select @SubResult = CostCentreID from tbl_costcentre_groupdetails where CostCentreID=@ID
	
	if @SubResult > 0 
		set @Result = 0
	
	Select @SubResult = CostCentreID from tbl_section_costcentres where CostCentreID=@ID
	
	if @SubResult > 0 
		set @Result = 0
	
		
	if @Result = 1
		Delete from tbl_costcentres where CostCentreID=@ID
			
	
	select @Result		
RETURN