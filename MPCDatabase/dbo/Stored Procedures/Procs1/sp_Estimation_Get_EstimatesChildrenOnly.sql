CREATE PROCEDURE dbo.sp_Estimation_Get_EstimatesChildrenOnly

AS
	SELECT tbl_estimates.Estimate_ID,tbl_estimates.Estimate_Code,tbl_estimates.status,tbl_estimates.flag, tbl_estimates.Estimate_Name, tbl_estimates.Estimate_Total, tbl_estimates.ParentID as ParentID,tbl_estimates.Version,tbl_customers.CustomerName,tbl_customercontacts.FirstName AS ContactName,tbl_systemusers.Fullname as SalesPerson  FROM tbl_customercontacts INNER JOIN tbl_estimates ON (tbl_customercontacts.CustomerContactID = tbl_estimates.Contact_ID) INNER JOIN tbl_customers ON (tbl_estimates.Customer_ID = tbl_customers.CustomerID) INNER JOIN tbl_systemusers ON (tbl_estimates.created_by = tbl_systemusers.systemuserid) where tbl_estimates.ParentID<>0 and tbl_estimates.IsEstimate=1
	RETURN