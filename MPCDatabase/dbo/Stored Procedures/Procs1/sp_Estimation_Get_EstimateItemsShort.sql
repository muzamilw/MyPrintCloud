CREATE PROCEDURE [dbo].[sp_Estimation_Get_EstimateItemsShort]--5611
(@EstimateID int)
AS
	Select isnull(IsGroupItem,0) as  IsGroupItem,isnull(ItemType,0) as ItemType, 
	ItemID,ItemCode,isnull(Title, productName) As Title,
	Round(Qty1Tax1Value,2) as VAT ,Round(Qty1NetTotal,2) as Total,
	isnull(FlagID, (Select top 1 SectionFlagID from tbl_section_flags where sectionID = 48)) As FlagID ,Round(Qty1MarkUp1Value,2) as ProfitMargin
                    FROM tbl_items 
                    where --tbl_items.status=17 and
                    tbl_items.EstimateID = @EstimateID 
                    order by tbl_items.ItemID
                    
	RETURN