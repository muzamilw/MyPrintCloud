CREATE PROCEDURE dbo.StoredProcedure1
AS

		SELECT     tbl_voucher.VoucherDate, tbl_voucher.Description, tbl_voucher.Reference, tbl_voucher.VoucherType, tbl_voucher.InvoiceType, 
	(case when  tbl_voucher.InvoiceType = 'SI'  then
	tbl_voucher.TotalAmount
else  
0
end) as Debit,
(case when  tbl_voucher.InvoiceType = 'SA' or tbl_voucher.InvoiceType = 'SC'  then
	tbl_voucher.TotalAmount
else  
0
end) as Credit,  tbl_systemusers.UserName, tbl_customers.AccountNumber, tbl_customers.CustomerName, tbl_customers.URL, tbl_customers.Terms, 
                      tbl_customeraddresses.AddressName, tbl_customeraddresses.Address1, tbl_customeraddresses.Address2, tbl_customeraddresses.Address3, 
                      tbl_customeraddresses.City, tbl_state.StateName, tbl_country.CountryName, tbl_customeraddresses.PostCode, tbl_customeraddresses.Fax, 
                      tbl_customeraddresses.Email, tbl_customeraddresses.URL AS Expr1, tbl_customeraddresses.Tel1, tbl_customeraddresses.Tel2, 
                      tbl_customeraddresses.Extension1, tbl_customeraddresses.Extension2, tbl_customeraddresses.Reference AS Expr2, tbl_customeraddresses.FAO, 
                      GETDATE() AS CurrentDate, tbl_customers.AccountBalance, tbl_company_sites.SiteName, tbl_company_sites.Address1 AS SiteAddress1, 
                      tbl_company_sites.Address2 AS SiteAddress2, tbl_company_sites.Address3 AS SiteAddress3, tbl_company_sites.City AS SiteCity, 
                      tbl_company_sites.ZipCode AS SiteZipCode, tbl_company_sites.Tel AS SiteTel, tbl_company_sites.Fax AS SiteFax, 
                      tbl_company_sites.Mobile AS SiteMobile, tbl_company_sites.Email AS SiteEmail, tbl_company_sites.URL AS SiteURL, tbl_company.CompanyName, 
                      tbl_company.Address1 AS CompanyAddress1, tbl_company.Address2 AS CompanyAddress2, tbl_company.Address3 AS CompanyAddress3, 
                      tbl_company.City AS CompanyCity, tbl_company.ZipCode AS CompanyZipCode, tbl_company.Tel AS CopmanyTel, 
                      tbl_company.Mobile AS CompanyMobile, tbl_company.Email AS CompanyEmail, tbl_company.Fax AS CompanyFax, 
                      tbl_company.URL AS CompanyURL,
                      (SELECT    SUM((case when  InvoiceType = 'SA' or InvoiceType = 'SC'  then -TotalAmount else TotalAmount end)) FROM     tbl_voucher where DATEDiff(month, tbl_voucher.VoucherDate,GetDate()) = 0   and (CSCode=tbl_customers.CustomerID and CSType='C')) as [Current] ,
                      (SELECT    SUM((case when  InvoiceType = 'SA' or InvoiceType = 'SC'  then -TotalAmount else TotalAmount end)) FROM     tbl_voucher where DATEDiff(month, tbl_voucher.VoucherDate,GetDate()) = 1   and (CSCode=tbl_customers.CustomerID and CSType='C')) as [30 day trial period] ,
                      (SELECT    SUM((case when  InvoiceType = 'SA' or InvoiceType = 'SC'  then -TotalAmount else TotalAmount end)) FROM     tbl_voucher where DATEDiff(month, tbl_voucher.VoucherDate,GetDate()) = 2   and (CSCode=tbl_customers.CustomerID and CSType='C')) as [60 day trial period] ,
                      (SELECT    SUM((case when  InvoiceType = 'SA' or InvoiceType = 'SC'  then -TotalAmount else TotalAmount end)) FROM     tbl_voucher where DATEDiff(month, tbl_voucher.VoucherDate,GetDate()) = 3   and (CSCode=tbl_customers.CustomerID and CSType='C'))	 as [90 day trial period],
                      (SELECT    SUM((case when  InvoiceType = 'SA' or InvoiceType = 'SC'  then -TotalAmount else TotalAmount end)) FROM     tbl_voucher where DATEDiff(month, tbl_voucher.VoucherDate,GetDate()) > 3   and (CSCode=tbl_customers.CustomerID and CSType='C'))	 as [Older]
                       
                      
FROM         tbl_company INNER JOIN
                      tbl_company_sites ON tbl_company.CompanyID = tbl_company_sites.CompanyID INNER JOIN
                      tbl_voucher INNER JOIN
                      tbl_customers ON tbl_voucher.CSCode = tbl_customers.CustomerID and tbl_voucher.CSType='C'  INNER JOIN
                      tbl_systemusers ON tbl_voucher.UserID = tbl_systemusers.SystemUserID INNER JOIN
                      tbl_customeraddresses ON tbl_customers.CustomerID = tbl_customeraddresses.CustomerID AND 
                     tbl_customeraddresses.IsDefaultAddress <> 0 INNER JOIN
                      tbl_state ON tbl_customeraddresses.StateID = tbl_state.StateID INNER JOIN
                      tbl_country ON tbl_customeraddresses.CountryID = tbl_country.CountryID ON tbl_company_sites.SiteID = tbl_voucher.SystemSiteID
                      
					/*(SELECT    SUM((case when  InvoiceType = 'SA' or InvoiceType = 'SC'  then -TotalAmount else TotalAmount end)) FROM     tbl_voucher where DATEDiff(month, tbl_voucher.VoucherDate,GetDate()) = 1  and (CSCode=400 and CSType='C'))*/
					
	RETURN