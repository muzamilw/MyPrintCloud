CREATE PROCEDURE dbo.sp_Customers_GetUserFullName
	(
      @SystemUserID int
	)

AS
	SELECT tbl_systemusers.FullName FROM tbl_systemusers 
        WHERE tbl_systemusers.SystemUserID=@SystemUserID
	RETURN