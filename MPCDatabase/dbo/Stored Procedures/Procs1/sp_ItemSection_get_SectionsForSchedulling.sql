CREATE PROCEDURE dbo.sp_ItemSection_get_SectionsForSchedulling

	(
			
		@PageNumber int = 1,
		@PageSize int = 10,
		@Status int
		
			
	)

AS

exec  Paging_RowCount  'tbl_item_sections','tbl_item_sections.ItemSectionID','  INNER JOIN tbl_items ON (tbl_items.ItemID = tbl_item_sections.ItemID)
																								INNER JOIN tbl_machines ON (tbl_item_sections.PressID = tbl_machines.MachineID) 
																								INNER JOIN tbl_machines as tbl_Guillotine  ON (tbl_item_sections.GuillotineID = tbl_Guillotine.MachineID) 
																								INNER JOIN tbl_estimates ON (tbl_items.EstimateID = tbl_estimates.Estimate_ID)
																								INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_estimates.SystemSiteID) 
																								INNER JOIN tbl_customers ON (tbl_estimates.Customer_ID = tbl_customers.CustomerID) '
																								,' tbl_item_sections.PressID, 
																								tbl_item_sections.ItemSectionID, 
																								tbl_item_sections.PressSpeed1, 
																								tbl_item_sections.PressSpeed2, 
																								tbl_item_sections.PressSpeed3, 
																								tbl_item_sections.SectionNo, 
																								tbl_items.JobCode, 
																								tbl_items.JobEstimatedStartDateTime, 
																								tbl_items.JobEstimatedCompletionDateTime ,
																								tbl_items.ItemID , 
																								tbl_items.ItemCode,  
																								tbl_items.JobEstimatedStartDateTime , 
																								tbl_items.JobEstimatedCompletionDateTime , 
																								tbl_items.JobCreationDateTime'
																								 ,NULL,@PageNumber,@PageSize
	
	RETURN