CREATE FUNCTION dbo.PrintlinkCategoryView
	(
	@parentID int
	)
RETURNS bit
AS
	BEGIN
		Declare @TotalC int 
		Declare @TotalCA int
		Declare @Result bit

		Set @Result=0	
		Set @TotalC=0
		Set @TotalCA=0
		
		Declare Cur cursor for SELECT     COUNT(ID) as TotalC,
															(SELECT Count(tbl_itemlibrary_catalogue_detail.ID) from tbl_itemlibrary_catalogue_detail 
															 inner join tbl_finishedgoodpricematrix on (tbl_finishedgoodpricematrix.ItemID=tbl_itemlibrary_catalogue_detail.ItemID)
															 where  tbl_finishedgoodpricematrix.CategoryID=@parentID)  as TotalCa
							FROM         tbl_finishgood_categories
																							WHERE     (ParentID = @parentid)

		open Cur
		
			Fetch next from Cur into @TotalC,@TotalCA
		
		Close Cur

		if @TotalCA>0 or @TotalC >0
		Begin
			Set @Result=1
		End

		RETURN @Result
	END