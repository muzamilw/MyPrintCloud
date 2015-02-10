CREATE PROCEDURE [dbo].[usp_GetProductsCatalogue_PriceQuantitybyCATEGORYID]
  @CATEGORYID int,
  @ProductID int
AS
BEGIN
	SELECT *
	FROM	tbl_items WPCI
	WHERE	ProductCategoryID = @CATEGORYID AND ItemID = @ProductID

END