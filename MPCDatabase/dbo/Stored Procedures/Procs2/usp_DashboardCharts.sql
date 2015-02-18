create PROCEDURE [dbo].[usp_DashboardCharts]
	@SelectedID int
AS
BEGIN
select 1 as Id,
(select COUNT(estimateID) from tbl_estimates
where orderManagerID = @SelectedID and isestimate = 1 and statusid in (select statusid from tbl_statuses where statusid != 9)) As Estimates,

(select COUNT(invoiceID) from tbl_invoices
where InvoicePostedBy = @SelectedID) As PostedInvoices,

(select COUNT(invoiceID) from tbl_invoices
where EstimateID in(select EstimateID from tbl_estimates where OrderManagerID = @SelectedID)) As Invoices,

(select COUNT(itemID) from tbl_items
where EstimateID in(select EstimateID from tbl_estimates where OrderManagerID = @SelectedID)
and status in(select StatusID from tbl_Statuses where StatusType = 3) and Status <> 17) As JobsInProgress

			RETURN 
	END