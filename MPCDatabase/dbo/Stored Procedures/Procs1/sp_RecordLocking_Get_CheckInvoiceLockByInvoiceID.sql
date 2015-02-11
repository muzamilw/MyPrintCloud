CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckInvoiceLockByInvoiceID
(
	@RecordID int
)
AS
	SELECT tbl_Invoices.LockedBy, tbl_Invoices.InvoiceID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers Right Outer Join tbl_Invoices ON (tbl_systemusers.SystemUserID = tbl_Invoices.LockedBy) where tbl_Invoices.InvoiceID =@RecordID
	RETURN