CREATE PROCEDURE [dbo].[sp_machines_delete]
(@MachineID int)
AS
	Declare @Result int
	Declare @SubResult int
	
	Set @Result = 1
	Set @SubResult = 0
	
	Select @SubResult = PressID from tbl_profile where PressID=@MachineID
		if @SubResult > 0 
			Set @Result = 0
	
	Select @SubResult = GuilotineID from tbl_profile where GuilotineID=@MachineID
		if @SubResult > 0 
			Set @Result = 0
	
	Select @SubResult = PressID from tbl_item_sections where PressID=@MachineID
		if @SubResult > 0 
			Set @Result = 0
	
	Select @SubResult = GuillotineID from tbl_item_sections where GuillotineID=@MachineID
		if @SubResult > 0 
			Set @Result = 0
	
	if @Result = 1
		Begin
			delete from tbl_machine_guilotine_ptv where GuilotineID=@MachineID
			Delete from tbl_machine_ink_coverage where MachineID=@MachineID
			Delete from tbl_machine_lookup_methods where MachineID=@MachineID
			Delete From tbl_machine_pagination_profile where MachineID=@MachineID
			Delete from tbl_machine_resource where MachineID=@MachineID
			Delete from tbl_machine_spoilage where MachineID=@MachineID
			delete from tbl_machine_costcentre_groups where MachineID = @MachineID
			Delete from tbl_machines where MachineID=@MachineID
		
		End
	
	Select @Result
	
	RETURN