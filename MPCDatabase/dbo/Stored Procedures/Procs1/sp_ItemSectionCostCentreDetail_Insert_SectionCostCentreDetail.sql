CREATE PROCEDURE [dbo].[sp_ItemSectionCostCentreDetail_Insert_SectionCostCentreDetail]
(
	@SectionCostCentreID int,
	@StockId int,
	@SupplierID int,
	@Qty1 float,
	@Qty2 float,
	@Qty3 float,
	@CostPrice float,
	@ActualQtyUsed int

)
AS
insert into tbl_section_costcentre_detail (SectionCostCentreID,StockId,SupplierID,Qty1,Qty2,Qty3,CostPrice,ActualQtyUsed) VALUES 
(@SectionCostCentreID,@StockId,@SupplierID,@Qty1,@Qty2,@Qty3,@CostPrice,@ActualQtyUsed)
	RETURN