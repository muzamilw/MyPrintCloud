CREATE PROCEDURE dbo.sp_DataCollection_Get_CheckInTime

	(
		@UserID int
		
	)

AS
	SELECT top 1 * FROM tbl_systemuser_checkins where UserID=@UserID and CheckoutDateTime is null and ReasonID is null order by ID desc
	RETURN