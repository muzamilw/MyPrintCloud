CREATE PROCEDURE dbo.sp_regionalSettings_get_states
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	Select * from tbl_state order by StateName
	RETURN