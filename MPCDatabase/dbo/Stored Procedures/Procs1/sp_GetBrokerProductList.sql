CREATE PROCEDURE sp_GetBrokerProductList
	@ContactCompanyID int
AS
BEGIN
SELECT     p.ItemID, CCItem.ContactCompanyID, CCItem.BrokerMarkup, CCItem.ContactMarkup, CCItem.isDisplayToUser, p.EstimateID, p.ProductName, PCat.CategoryName AS ProductCategoryName, p.ProductCategoryID, PCat.ParentCategoryID, 
                       Case  
                      when  (select count (*) from tbl_items_pricematrix where contactcompanyID = @ContactCompanyID and itemid = p.itemid) <> 0  
                      Then (SELECT IsNull(dbo.fn_GetBrokerProductMinPrice(p.ItemID, @ContactCompanyID),0.0)) 
                      
                      when (select count (*) from tbl_items_pricematrix where contactcompanyID = @ContactCompanyID and itemid = p.itemid) = 0  
                      Then (Select ISNULL(dbo.funGetMiniumProductValue(p.ItemID), 0.0) AS MinPrice)
                      End 
                      AS MinPrice, p.ImagePath, p.ThumbnailPath, p.IconPath, p.IsEnabled, 
                      p.IsPublished, p.IsFinishedGoods, p.ProductSpecification, p.CompleteSpecification, p.TipsAndHints, p.IsArchived, p.InvoiceID, p.TemplateID, 
                      dbo.GetTopCategoryID(p.ProductCategoryID) AS TopCategoryID, isnull(p.SortOrder, 0) As SortOrder, p.isQtyRanged, p.isMarketingBrief, p.PriceDiscountPercentage
FROM         dbo.tbl_items AS p inner join
                      dbo.tbl_ProductCategory AS PCat ON PCat.ProductCategoryID = p.ProductCategoryID
                      inner join dbo.tbl_ContactCompanyItems CCItem on CCItem.ItemID = p.ItemID
WHERE     PCat.isArchived = 0 and p.IsPublished = 1 and CCItem.Contactcompanyid = @ContactCompanyID
END