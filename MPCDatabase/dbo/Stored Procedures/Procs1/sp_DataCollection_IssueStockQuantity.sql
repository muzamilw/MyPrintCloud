CREATE PROCEDURE dbo.sp_DataCollection_IssueStockQuantity
(
	@ItemID int,
	@SectionCostCentreID int,
	@SectionCostCentreDetailID int,
	@IssuedQty float,
	@StockIssueBy int,
	@StockID int,
	@StockIssueDate datetime
)
AS
	Declare @TotalQuantity int
	set @TotalQuantity =0
	
	insert into tbl_stockitems_issue_log (itemid,SectionCostCentreID,SectionCostCentreDetailID,IssuedQty,IssuedBy,StockID,IssueDateTime) VALUES 
     (@ItemID,@SectionCostCentreID,@SectionCostCentreDetailID,@IssuedQty,@StockIssueBy,@StockID,@StockIssueDate)

    select @TotalQuantity=sum(IssuedQty) from tbl_stockitems_issue_log where SectionCostCentreDetailID=@SectionCostCentreDetailID and StockID=@StockId
     
    update tbl_section_costcentre_detail set ActualQtyUsed=@TotalQuantity where SectionCostCentreDetailID=@SectionCostCentreDetailId
	RETURN