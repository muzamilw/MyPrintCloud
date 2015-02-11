CREATE PROCEDURE [dbo].[usp_GetWebStates]
(
	@CountryID int = null
)

AS
BEGIN
		select StateID, CountryID, StateCode, StateName
		from tbl_state
		where CountryID = @CountryID
		
END