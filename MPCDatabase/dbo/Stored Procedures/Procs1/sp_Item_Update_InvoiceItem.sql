--- The Sp is only for Item 

CREATE PROCEDURE dbo.sp_Item_Update_InvoiceItem

	(
		
@InvoiceDescription as text,
@ItemID int

	)

AS
	
	update tbl_items set 
		
				InvoiceDescription = @InvoiceDescription 
			
	       where ItemID=@ItemID
	RETURN