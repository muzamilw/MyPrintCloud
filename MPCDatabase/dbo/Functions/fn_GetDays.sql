

-- =============================================
-- Author:		Muhammad Naveed
-- Create date: 7/09/2012
-- Description:	Get days from the Campaign Unordered Cart
-- =============================================
create FUNCTION [dbo].[fn_GetDays]

()
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Days  int

	select top 1 @Days = SendEmailAfterDays from tbl_campaigns where CampaignType = 2 and EmailEvent = 3

	-- Return the result of the function
	RETURN @Days

END