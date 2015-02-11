CREATE PROCEDURE dbo.sp_ItemSectionCostCentreDetail_Update_Details
(
	@SectionCostCentreID int,
	@StockId int,
	@SupplierID int,
	@Qty1 float,
	@Qty2 float,
	@Qty3 float,
	@CostPrice float,
	@ActualQtyUsed  int,
	@SectionCostCentreDetailId int
)
AS
	update tbl_section_costcentre_detail set SectionCostCentreID=@SectionCostCentreID,StockId=@StockId,SupplierID=@SupplierID,Qty1=@Qty1,
		Qty2=@Qty2,Qty3=@Qty3,CostPrice=@CostPrice,ActualQtyUsed=@ActualQtyUsed where SectionCostCentreDetailId=@SectionCostCentreDetailId
	RETURN