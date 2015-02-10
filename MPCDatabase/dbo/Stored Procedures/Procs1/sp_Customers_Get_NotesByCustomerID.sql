CREATE PROCEDURE [dbo].[sp_Customers_Get_NotesByCustomerID]
(
	@CustomerID int
)
AS
	SELECT Notes,NotesLastUpdatedDate,tbl_systemusers.Fullname as NotesLastUpdatedBy from tbl_ContactCompanies 
	 Inner join tbl_systemusers on ( tbl_ContactCompanies.NotesLastUpdatedBy =  tbl_systemusers.SystemuserID ) 
	 WHERE ContactCompanyID=@CustomerID
	RETURN