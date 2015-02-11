
-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 07/09/2012
-- Description:	Get Customer count for the campaign UnorderedCart
-- =============================================
create FUNCTION [dbo].[fn_GetCustomerCount] ()

RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @iCustomerCount int
	
	select @iCustomerCount = COUNT(cc.ContactCompanyID) 
	from tbl_contactcompanies cc
	inner join tbl_estimates es on es.ContactCompanyID = cc.ContactCompanyID
	where datediff (day,cc.CreationDate, GETDATE()) < dbo.fn_GetDays()
	and isnull(es.isEmailSent,0) = 0
	and es.StatusID = 3
	
	
	-- Return the result of the function
	RETURN @iCustomerCount

END