CREATE PROCEDURE [dbo].[sp_lookupmethod_delete]
(@MethodID int)
AS
		
		
	Declare @Result int
	Declare @SubResult int
	
	Set @Result = 1
	Set @SubResult = 0
	
	Select @SubResult = MethodID from tbl_machine_lookup_methods where MethodID=@MethodID
	if @SubResult > 0 
			Set @Result = 0
		
	
	Select @SubResult = LookupMethodID from tbl_pagination_profile where LookupMethodID=@MethodID
	if @SubResult > 0 
			Set @Result = 0
		
	Select @SubResult = SelectedPressCalculationMethodID from tbl_item_sections where SelectedPressCalculationMethodID=@MethodID
	if @SubResult > 0 
			Set @Result = 0
			
    select @SubResult = MethodID from tbl_machine_clickchargelookup where MethodID = @MethodID
    if @SubResult > 0
			Set @Result = 0
			
	select @SubResult = MethodID from tbl_machine_speedweightlookup where MethodID = @MethodID
    if @SubResult > 0
			Set @Result = 0
			
		
	select @SubResult = MethodID from tbl_machine_clickchargezone where MethodID = @MethodID
    if @SubResult > 0
			Set @Result = 0
			
					
	select @SubResult = MethodID from tbl_machine_guillotinecalc where MethodID = @MethodID
    if @SubResult > 0
			Set @Result = 0		
				
				
    select @SubResult = MethodID from tbl_machine_meterperhourlookup where MethodID = @MethodID
    if @SubResult > 0
			Set @Result = 0	
			
			
			
	select @SubResult = MethodID from tbl_machine_perhourlookup where MethodID = @MethodID
    if @SubResult > 0
			Set @Result = 0	
					
							
	if @Result = 1	
		delete FROM tbl_lookup_methods WHERE tbl_lookup_methods.MethodID = @MethodID
         
  
  Select @Result       
                 RETURN