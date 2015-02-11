CREATE PROCEDURE [dbo].[sp_RecordLocking_Get_CheckSupplierLockBySupplierID]
(
	@RecordID int
)
AS
	SELECT tbl_ContactCompanies.LockedBy, tbl_ContactCompanies.ContactCompanyID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP 
	FROM tbl_systemusers RIGHT OUTER JOIN tbl_ContactCompanies ON (tbl_systemusers.SystemUserID = tbl_ContactCompanies.LockedBy) 
	where tbl_ContactCompanies.ContactCompanyID = @RecordID
	RETURN