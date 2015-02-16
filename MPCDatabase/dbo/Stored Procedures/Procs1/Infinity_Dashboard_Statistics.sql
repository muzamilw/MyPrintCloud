
CREATE PROCEDURE Infinity_Dashboard_Statistics
@UserID int
As
Select( SELECT     count(DeliveryNoteID) 
FROM tbl_deliverynotes 
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_deliverynotes.SystemSiteID)
INNER JOIN tbl_ContactCompanies ON (tbl_deliverynotes.ContactCompanyID = tbl_ContactCompanies.ContactCompanyID) 
INNER JOIN tbl_addresses ON (tbl_deliverynotes.AddressID = tbl_addresses.AddressID) 
where IsStatus=1 and tbl_company_sites.companysiteid = 1
) as DeliveriesAwaiting,

(SELECT     count(EnquiryID) 
FROM tbl_enquiries 
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_enquiries.SystemSiteID)  
where Status=3 and SalesPersonID=@UserID
) as EnquiriesCompleted,

(SELECT     count(EnquiryID) FROM tbl_enquiries 
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_enquiries.SystemSiteID)  where Status=0 and tbl_company_sites.companysiteid = 1
) as EnquiriesUnAssignedOnline,

(SELECT     count(EstimateID)  FROM         tbl_estimates  INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID)  WHERE  IsEstimate = 1 AND StatusID = 1 and tbl_company_sites.companysiteid = 1
) as EstimtingPendingEstimates
,
(SELECT     count(InvoiceID)  FROM         tbl_invoices   INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_invoices.SystemSiteID)    INNER JOIN tbl_contacts ON tbl_invoices.ContactID = tbl_contacts.ContactID    INNER JOIN  tbl_contactcompanies ON tbl_invoices.contactcompanyid = tbl_contactcompanies.contactcompanyid   WHERE  InvoiceStatus= 19  and (IsArchive=0) and tbl_company_sites.companysiteid = 1)
as InvoicesAwaiting,

(SELECT     COUNT(*) AS Expr1 FROM         tbl_items INNER JOIN tbl_estimates ON tbl_items.EstimateID = tbl_estimates.EstimateID INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID) RIGHT OUTER JOIN tbl_item_sections ON (tbl_item_sections.ItemID = tbl_items.ItemID) LEFT OUTER JOIN tbl_contactcompanies ON (tbl_estimates.ContactCompanyID = tbl_contactcompanies.ContactCompanyID) 
LEFT OUTER JOIN tbl_addresses ON (tbl_estimates.AddressID = tbl_addresses.AddressID) 
LEFT OUTER JOIN tbl_contacts ON (tbl_estimates.ContactID = tbl_contacts.ContactID) 
INNER JOIN tbl_statuses ON (tbl_items.status = tbl_statuses.StatusID) 
WHERE     (tbl_items.Status =  12 or tbl_items.Status =  13 or tbl_items.Status =  14  and tbl_company_sites.companysiteid = 1)
) as JobsInProgress
,
(SELECT     COUNT(*) AS Expr1 FROM         tbl_items INNER JOIN tbl_estimates ON tbl_items.EstimateID = tbl_estimates.EstimateID 
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID)
RIGHT OUTER JOIN tbl_item_sections ON (tbl_item_sections.ItemID = tbl_items.ItemID) 
LEFT OUTER JOIN tbl_contactcompanies ON (tbl_estimates.ContactCompanyID = tbl_contactcompanies.ContactCompanyID) 
LEFT OUTER JOIN tbl_addresses ON (tbl_estimates.AddressID = tbl_addresses.AddressID) 
LEFT OUTER JOIN tbl_contacts ON (tbl_estimates.ContactID = tbl_contacts.ContactID)
INNER JOIN tbl_statuses ON (tbl_items.status = tbl_statuses.StatusID)
WHERE     (tbl_items.Status =  11 )  and tbl_company_sites.companysiteid = 1) as JobsNotStarted, -- Need Assigning

(SELECT     COUNT(*) AS Expr1 FROM  tbl_items INNER JOIN tbl_estimates ON tbl_items.EstimateID = tbl_estimates.EstimateID 
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID) 
RIGHT OUTER JOIN tbl_item_sections ON (tbl_item_sections.ItemID = tbl_items.ItemID) 
LEFT OUTER JOIN tbl_contactcompanies ON (tbl_estimates.ContactCompanyID = tbl_contactcompanies.ContactCompanyID) 
LEFT OUTER JOIN tbl_addresses ON (tbl_estimates.AddressID = tbl_addresses.AddressID) 
LEFT OUTER JOIN tbl_contacts ON (tbl_estimates.ContactID = tbl_contacts.ContactID) 
INNER JOIN tbl_statuses ON (tbl_items.status = tbl_statuses.StatusID) WHERE     (tbl_items.Status =  15 )  and  tbl_company_sites.companysiteid = 1)
 as JobsOnHold -- Ready For Shipping
,
(SELECT     count(*)   from tbl_campaigns WHERE([Private] = 1 AND tbl_campaigns.UID = @UserID AND EnableSchedule<>0) OR ([Private] = 0 AND EnableSchedule<>0)
) as MarketingScheduleCompaigns,
(SELECT     count(*)  FROM         tbl_estimates  INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID)  
WHERE  IsEstimate = 0 AND tbl_estimates.StatusID = 5 and tbl_company_sites.companysiteid = 1
) as ConfirmedOrders
,
(SELECT     count(*)  FROM         tbl_estimates  INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID)  
WHERE  IsEstimate = 0 AND tbl_estimates.StatusID = 6 and tbl_company_sites.companysiteid = 1
) as InProductionOrders
,
(SELECT     count(*)  FROM         tbl_estimates  INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID)  
WHERE  IsEstimate = 0 AND tbl_estimates.StatusID = 7 and tbl_company_sites.companysiteid = 1
) as ToBeInvoicedOrders -- Ready forShipping
,
(SELECT     count(*)  FROM         tbl_estimates  INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_estimates.CompanySiteID)  
WHERE  IsEstimate = 0 AND tbl_estimates.StatusID = 4 and tbl_company_sites.companysiteid = 1
) as PendingOrders
,
(SELECT     COUNT(*)  FROM         tbl_purchase  INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_purchase.SystemSiteID) 
INNER JOIN tbl_systemusers ON (tbl_purchase.UserID = tbl_systemusers.SystemUserID)  INNER JOIN tbl_ContactCompanies ON (tbl_purchase.SupplierID = tbl_ContactCompanies.ContactCompanyID) 
WHERE  tbl_purchase.Status = 31 and tbl_company_sites.companysiteid = 1 ) as PurchasesAwaitingPO
,
(SELECT     count(*) FROM         tbl_goodsreceivednote   
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_goodsreceivednote.SystemSiteID)
INNER JOIN tbl_systemusers ON (tbl_goodsreceivednote.UserID = tbl_systemusers.SystemUserID)  
INNER JOIN tbl_ContactCompanies ON (tbl_goodsreceivednote.SupplierID = tbl_ContactCompanies.ContactCompanyID)
WHERE  tbl_goodsreceivednote.Status = 31 and tbl_company_sites.companysiteid = 1) as  PurchasesAwaitingGRN
,
(SELECT     count(*) FROM         tbl_tasks where status <> 3 and (owner= @UserID OR CreatedBy= @UserID) ) as CRMTasks
,
(SELECT     count(*) FROM         tbl_activity WHERE (IsComplete = 0) AND (systemUserID = @UserID or CreatedBy= @UserID)) as CRMActivities