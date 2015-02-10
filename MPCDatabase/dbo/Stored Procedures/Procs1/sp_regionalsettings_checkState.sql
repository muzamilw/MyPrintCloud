CREATE PROCEDURE dbo.sp_regionalsettings_checkState
(@StateID  int,
@StateName varchar(50),
@StateCode varchar(50)
)
AS
Declare @Check int
Set @Check=0
 Select @Check=StateID from tbl_state where StateID<>@StateID and (StateCode=@StateCode)
 
 if (@Check > 0)
	begin
		Set @Check=1
	end
 else
	begin
		Select @Check=StateID from tbl_state where StateID<>@StateID and (StateName=@StateName)
	end
RETURN