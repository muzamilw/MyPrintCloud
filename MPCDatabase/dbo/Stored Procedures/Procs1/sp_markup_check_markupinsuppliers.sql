CREATE PROCEDURE [dbo].[sp_markup_check_markupinsuppliers]
(@MarkUpID int)
AS

SELECT tbl_ContactCompanies.ContactCompanyID FROM tbl_ContactCompanies 
         WHERE ((tbl_ContactCompanies.DefaultMarkUpID = @MarkUpID) And (tbl_contactCompanies.IsCustomer = 2))
        RETURN