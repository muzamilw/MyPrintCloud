CREATE PROCEDURE dbo.sp_copmany_Copy_FinishGood_Others
(
	@OldSiteID int,
	@NewSiteID int,
	@DepartmentID int
)
AS

		Declare @SectionCostCentreDetailID int
		Declare @FinishGoodID int

		--Update FinishGoods Supplier
		update tbl_finishedgoods set SupplierID=IsNull((Select [New_ID] from #Suppliers where Old_ID=SupplierID),0),
								 SiteDepartmentID= @DepartmentID
								 where SystemSiteID=@NewSiteID
	
	
	
		Declare FinishGoodCursor Cursor for SELECT ItemID from tbl_finishedgoods WHERE     (tbl_finishedgoods.SystemSiteID = @NewSiteID)
		
						
		open FinishGoodCursor
		FETCH NEXT FROM FinishGoodCursor into @FinishGoodID
		while @@FETCH_STATUS = 0 
		Begin
		
			update tbl_finishedgoodpricematrix set 
			CustomerID=IsNull((Select [New_ID] from #Customers where Old_ID=CustomerID),0),
			CategoryID=IsNull((Select [New_ID] from #CustomerCategories where Old_ID=CategoryID),0) 
			where ItemID=@FinishGoodID				
			FETCH NEXT FROM FinishGoodCursor into @FinishGoodID
		End
	close FinishGoodCursor 
	Deallocate FinishGoodCursor 
			
	--Update SectionCostCentreDetail Supplier			
	Declare DetailCursor Cursor for SELECT     tbl_section_costcentre_detail.SectionCostCentreDetailId
								FROM         tbl_items INNER JOIN
								tbl_item_sections ON tbl_items.ItemID = tbl_item_sections.ItemID INNER JOIN
								tbl_finishedgoods ON tbl_items.ItemID = tbl_finishedgoods.ItemID INNER JOIN
								tbl_section_costcentres ON tbl_item_sections.ItemSectionID = tbl_section_costcentres.SectionID INNER JOIN
								tbl_section_costcentre_detail ON tbl_section_costcentre_detail.SectionCostCentreID = tbl_section_costcentres.SectionCostcentreID
								WHERE     (tbl_finishedgoods.SystemSiteID = @OldSiteID)
							
		open DetailCursor 
		FETCH NEXT FROM DetailCursor into @SectionCostCentreDetailID
		while @@FETCH_STATUS = 0 
		Begin
					
								update tbl_section_costcentre_detail set SupplierID=IsNull((Select [New_ID] from #Suppliers where Old_ID=SupplierID),0)
								 where SectionCostCentreDetailId=@SectionCostCentreDetailID
			FETCH NEXT FROM DetailCursor into @SectionCostCentreDetailID
		End
	close DetailCursor 
	Deallocate DetailCursor 
return