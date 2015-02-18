CREATE PROCEDURE sp_Orders_Get_OrderDetailRefDataByID
	@OrderID int,
	@UserID int
AS
BEGIN

exec sp_Orders_Get_OrderByID @OrderID
Declare @customerID int 
Set @CustomerID = (select ContactCompanyID from tbl_estimates where estimateid = @OrderID)
exec sp_Customers_GetCustomerContactsByCustomerID @CustomerID

Declare @user int
Set @user  = ( select isNull(OfficialOrderSetBy,0) as OfficialOrderSetBy from tbl_estimates where estimateid = @OrderID )

IF @user <> 0
Begin
exec sp_users_get_user_by_id @user
End
Else
Begin
exec sp_users_get_user_by_id @UserID
End

Set @user  = ( select isNull(CreditLimitSetBy,0) as CreditLimitSetBy from tbl_estimates where estimateid = @OrderID )

IF @user <> 0
Begin
exec sp_users_get_user_by_id @user
End
Else
Begin
exec sp_users_get_user_by_id @UserID
End

Set @user  = ( select isNull(AllowJobWOCreditCheckSetBy,0) as AllowJobWOCreditCheckSetBy from tbl_estimates where estimateid = @OrderID )

IF @user <> 0
Begin
exec sp_users_get_user_by_id @user
End
Else
Begin
exec sp_users_get_user_by_id @UserID
End

exec sp_Orders_Get_ItemsShortByEstimateID @OrderID

exec sp_Invoice_checkbyestimate @OrderID

exec sp_Orders_Get_PrePayments @OrderID
END