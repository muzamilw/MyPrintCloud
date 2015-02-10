
CREATE FUNCTION [dbo].[fn_GetOrderItemsList]

(

	@OrderID int,
	@itemID int
	

)

RETURNS varchar(1000)

AS

BEGIN

	-- Declare the return variable here

	DECLARE @otherItems  varchar(1000)

		set @otherItems = ''

		select @otherItems = @otherItems + convert(varchar(100), ROW_NUMBER() 

				OVER (ORDER BY ItemID)) + ': ' + ProductName + ' ' + isnull(ItemCode,'') + ' '  + s.StatusName  + CHAR(13)

		from tbl_items i

			inner join tbl_statuses s on i.Status = s.StatusID

		where estimateID = @OrderID and itemid <> @itemID

	

	RETURN @otherItems



END