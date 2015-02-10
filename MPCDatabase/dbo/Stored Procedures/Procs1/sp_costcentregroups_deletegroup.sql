CREATE PROCEDURE dbo.sp_costcentregroups_deletegroup
(
@GroupID int)
                  AS
Declare @Return bit
Declare @Result int

set  @Return = 1

---Check CostCetres in a group
SELECT @Result=GroupID FROM tbl_costcentre_groupdetails where CostCentreID=@GroupID             
if (@Result > 0)
	begin 
		set @Return=0
		select @Return
	return
	end   


---Check costcentre groups in machine before deletion                                  
SELECT @Result=ID FROM tbl_machine_costcentre_groups where CostCentreGroupID=@GroupID             
if (@Result > 0)
	begin 
		set @Return=0
		select @Return
	return
end   

---Check CostCentre Groups in products before deletion.
SELECT @Result=ID FROM tbl_profile_costcentre_groups where ProfileID=@GroupID             
if (@Result > 0)
	begin 
		set @Return=0
		select @Return
	return
end   

---Check CostCentre Groups in pagination before deletion.
SELECT @Result=ID FROM tbl_pagination_profile_costcentre_groups where CostcentreGroupID=@GroupID             
if (@Result > 0)
	begin 
		set @Return=0
		select @Return
	return
	end   

if (@Return=1)
 begin
	Delete from tbl_costcentre_groups where GroupID=@GroupID
 end
select @Return
return