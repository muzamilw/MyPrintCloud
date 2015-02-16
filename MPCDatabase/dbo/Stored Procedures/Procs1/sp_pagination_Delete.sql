CREATE PROCEDURE dbo.sp_pagination_Delete
(
@ID int)
AS
	
	Declare @Result int
	Declare @SubResult int
	
	Set @Result = 1
	Set @SubResult = 0
	
	Select @SubResult=PaginationID from tbl_machine_pagination_profile where PaginationID =@ID
	
	if @SubResult > 0 
		set @Result = 0
	
	if @Result = 1
		Begin
			Delete from tbl_pagination_profile where ID=@ID
			Delete From tbl_pagination_profile_costcentre_groups where PaginationID=@ID
		End 
		
	select @Result
	
RETURN