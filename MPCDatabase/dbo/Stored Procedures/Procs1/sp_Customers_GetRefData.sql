
CREATE PROCEDURE sp_Customers_GetRefData 
	@SectionID int,
	@CompanyID int,
	@CustomerID int,
	@UserID int
AS
BEGIN
	exec sp_SectionFlags_Get_Particular_Section_Flags @SectionID,1,@CompanyID -- customer section flags
	exec sp_Customers_Customer_GetParaentCompany @CustomerID -- parent company
	exec sp_Customer_CustomerTypes_GetCustomerTypes -- customertypes
	
	--GetDefaulult Nominal Code for sales
	exec sp_ChartOfAccounts_get_AccountListByType 4 ,3, 'tbl_company_sites.companysiteid in (1)'
	
	--Get Users
	SELECT tbl_systemusers.SystemUserID,tbl_systemusers.UserName,tbl_systemusers.UserType,tbl_systemusers.Description,
	tbl_systemusers.Password,tbl_systemusers.FullName, tbl_systemusers.Email,tbl_systemusers.Mobile,
	tbl_systemusers.RoleID,tbl_systemusers.IsAccountDisabled,tbl_systemusers.IsTillSupervisor,
	tbl_company.CompanyName, tbl_company_sites.CompanySiteName,tbl_roles.RoleName 
	FROM tbl_company 
	INNER JOIN tbl_systemusers ON (tbl_company.CompanyID = tbl_systemusers.OrganizationID) 
	INNER JOIN tbl_roles ON (tbl_systemusers.RoleID = tbl_roles.RoleID) 
	INNER JOIN tbl_company_sites ON (tbl_systemusers.CompanySiteID = tbl_company_sites.CompanySiteID) 
	Where tbl_systemusers.IsAccountDisabled = 0 And tbl_Company_Sites.companysiteid in (1)
	ORDER BY tbl_systemusers.FullName
	
	exec sp_Customers_Customers_GetCustomerDByCustomerID @CustomerID --Get Customer Detail Row
	
	--Get Customer Activities
	select tbl_activity.activityid,
	tbl_activity.activitytypeid,tbl_activitytype.ActivityName,tbl_activity.activitycode,
	tbl_activity.activityref,tbl_activity.activitydate,tbl_activity.activitytime,tbl_activity.activitystarttime,
	tbl_activity.activityendtime,tbl_activity.activitynotes,tbl_activity.isactivityalarm,
	tbl_activity.alarmdate,tbl_activity.alarmtime,tbl_activity.activitylink,
	tbl_activity.iscustomeractivity,tbl_activity.ContactCompanyid,tbl_activity.ContactID,tbl_activity.prospectcontactid,tbl_activity.suppliercontactid, 
	tbl_activity.systemuserid,
	tbl_activity.isprivate,tbl_activity.lastmodifieddate,tbl_activity.lastmodifiedtime,tbl_activity.IsComplete,
	tbl_activity.lastmodifiedby FROM tbl_activity 
	INNER JOIN tbl_activitytype ON tbl_activitytype.ActivityTypeID= tbl_activity.ActivityTypeID 
	WHERE ((tbl_activity.SystemUserID=@UserID OR tbl_activity.CreatedBy=@UserID))
	
	--ContactDetail of Customer
	exec sp_Customers_Contacts_GetContactsDByCustomerID @CustomerID
END