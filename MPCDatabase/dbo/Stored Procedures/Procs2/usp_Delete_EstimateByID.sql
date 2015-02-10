-- =============================================
-- Author:		Muzzammil
-- Create date: 20 noc 2013
-- Description:	Delete an order/Estimate by Order/EstimateID
-- =============================================
CREATE PROCEDURE [dbo].[usp_Delete_EstimateByID] 
	-- Add the parameters for the stored procedure here
	@EstimateID int = 0
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   --deleting the template objects
   
				DELETE tob
				FROM TemplateObject tob
				inner join TemplatePage topg on topg.ProductPageId = tob.ProductPageId
				inner join Template t on t.ProductID = topg.productid
				INNER JOIN Items i ON i.TemplateId = t.ProductId
				inner join Estimate E on E.EstimateId = i.EstimateId
				
				where E.EstimateID = @EstimateID

				--deleting the template pages
				DELETE topg
				FROM TemplatePage topg
				inner join Template t on t.ProductID = topg.productid
				INNER JOIN Items i ON i.TemplateId = t.ProductId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID
				
				--deleting the template images
				DELETE topg
				FROM TemplateBackgroundImage topg
				inner join Template t on t.ProductId = topg.productid
				INNER JOIN Items i ON i.TemplateId = t.ProductId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID
				


				--deleting the templates
				DELETE t
				FROM template t
				INNER JOIN Items i ON i.TemplateId = t.ProductId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID

				--deletinng the item section cost center detail
				delete  scd
				from dbo.SectionCostCentreDetail scd
				inner join dbo.SectionCostcentre sc on sc.SectionCostcentreId = scd.SectionCostCentreId
				inner join ItemSection iss on iss.ItemSectionId = sc.ItemSectionId
				INNER JOIN Items i ON i.ItemId = iss.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID
				
				





				--deletinng the item section cost center resources
				delete  scr
				from dbo.SectionCostCentreResource scr
				inner join dbo.SectionCostcentre sc on sc.SectionCostcentreId = scr.SectionCostCentreId
				inner join ItemSection iss on iss.ItemSectionId = sc.ItemSectionId
				INNER JOIN Items i ON i.ItemId = iss.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID


				--deletinng the item section cost center
				delete  sc
				from dbo.SectionCostcentre sc
				inner join ItemSection iss on iss.ItemSectionId = sc.ItemSectionId
				INNER JOIN Items i ON i.ItemId = iss.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID




				--deletinng the item section ink coverage
				delete  sink
				from dbo.SectionInkCoverage sink
				inner join ItemSection iss on iss.ItemSectionId = sink.SectionId
				INNER JOIN Items i ON i.ItemId = iss.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID

				--deletinng the item section
				delete  isec
				from dbo.ItemSection isec
				INNER JOIN Items i ON i.ItemId = isec.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID

				--deletinng the item attachments
				delete  isa
				from dbo.ItemAttachment isa
				INNER JOIN Items i ON i.ItemId = isa.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID




				--deletinng the item addon Cost Centres
				delete  iacc
				from dbo.ItemAddonCostCentre iacc
				INNER JOIN ItemStockOption stockOp ON stockOp.ItemStockOptionId = iacc.ItemStockOptionId
				INNER JOIN Items i ON i.ItemId = stockOp.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID


				--deletinng the item Price Matrix
				delete  ipm
				from dbo.ItemPriceMatrix ipm
				INNER JOIN Items i ON i.ItemId = ipm.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID


				--deletinng the item related items
				delete  iri
				from dbo.ItemRelatedItem iri
				INNER JOIN Items i ON i.ItemId = iri.ItemId
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID

				
				--deletinng the items
				delete  i
				from dbo.Items i
				inner join Estimate E on E.EstimateId = i.EstimateId
				where E.EstimateId = @EstimateID


				--deletinng the prepayments against estimates/orders
				delete  pp
				from dbo.PrePayment pp
				inner join Estimate E on E.EstimateId = pp.OrderID
				where E.EstimateId = @EstimateID


				--deletinng the estimates/orders
				delete  E
				from dbo.Estimate E
				where E.EstimateId = @EstimateID
END