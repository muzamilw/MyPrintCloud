CREATE PROCEDURE sp_Estimation_Get_RefData
@EnquiryID int
AS
BEGIN
exec sp_PipeLine_Get_SuccessChances
exec sp_PipeLine_Get_AllProducts
exec sp_PipeLine_Get_AllSources

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
	

END