
create FUNCTION [dbo].[fn_GetItemAttachmentsList]

(
	@RecordID int,
	@isOrder bit
)

RETURNS varchar(1000)

AS

BEGIN

	-- Declare the return variable here

	DECLARE @otherAttachments  varchar(1000)

		set @otherAttachments = ''
	if(@isOrder = 1)-- Attchments of an order
		Begin
			select @otherAttachments = @otherAttachments + convert(varchar(100), ROW_NUMBER() 

				OVER (ORDER BY ID)) + ': ' + isnull(convert(varchar(255), [FileName]),'') + ' ' + CHAR(13)

		from tbl_item_attachments i
		where itemid in(select itemid from tbl_items where estimateid = @RecordID)
		
		End
	Else -- Attachments List of an Item
		Begin
		select @otherAttachments = @otherAttachments + convert(varchar(100), ROW_NUMBER() 

				OVER (ORDER BY ID)) + ': ' + isnull(convert(varchar(255), [FileName]),'') + ' ' + CHAR(13)

		from tbl_item_attachments i

		--print @otherAttachments
		where itemid = @RecordID
		End

		

	

	RETURN @otherAttachments



END