-- =============================================
-- Author:		Naveed
-- Create date: 18-7-2013
-- Description:	Delete a company
-- =============================================
create PROCEDURE [dbo].[usp_DeleteContactCompanyByID]
	@SelectedID int
AS
	BEGIN
		DECLARE @TVP AS ContactCompaniesToBeDeleted;

		/* Add data to the table variable. */
		INSERT INTO @TVP (ContactCompanyID)
		select    ContactCompanyID from tbl_contactcompanies
		where	ContactcompanyID = @SelectedID

		/* Pass the table variable data to a stored procedure. */
		EXEC [usp_Delete_ContactCompany] @TVP;
	END