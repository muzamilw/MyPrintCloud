CREATE PROCEDURE [dbo].[sp_markup_check_markupincustomers]
(@MarkUpID int)
AS
	SELECT tbl_ContactCompanies.ContactCompanyID FROM tbl_ContactCompanies 
         WHERE ((tbl_ContactCompanies.DefaultMarkUpID = @MarkUpID) And (tbl_ContactCompanies.IsCustomer = 1))
        RETURN