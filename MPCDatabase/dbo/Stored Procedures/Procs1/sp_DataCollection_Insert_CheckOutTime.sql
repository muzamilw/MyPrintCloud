CREATE PROCEDURE dbo.sp_DataCollection_Insert_CheckOutTime

	(
		@UserID int,
		@CheckOutDateTime datetime,
		@ReasonID int
	)

AS
Begin
Declare @ID int
set @ID = -1
Select top 1 @ID=ID from  tbl_systemuser_checkins where UserID=@UserID and CheckOutDateTime is null and ReasonID is null Order by ID DESC

--insert a new record if not found
if (@ID = -1 )
Begin
	insert into tbl_systemuser_checkins (UserID,CheckOutDateTime,ReasonID) VALUES (@UserID,@CheckOutDateTime,@ReasonID)
end
ELSE
Begin
	update tbl_systemuser_checkins set CheckOutDateTime=@CheckOutDateTime,ReasonID=@ReasonID where ID=@ID
END

	
	END
	RETURN