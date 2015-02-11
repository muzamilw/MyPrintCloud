CREATE PROCEDURE sp_get_general_settings_DTO 
	@RoleID int,
	@UserID int,
	@CompanyID int,
	@SiteID int
AS
BEGIN
	
exec sp_roles_get_SQL_GET_ROLE_BY_ID @RoleID -- GetRolesByRolesID
exec sp_roles_get_ROLE_SITES @RoleID --RoleSitesTable
exec sp_companies_get_byid @CompanyID -- GetCompanyDetails
exec sp_companies_get_sitebyid @SiteID -- companysiteid
exec sp_companies_site_departments @SiteID --SiteDepartmentForCompanySite
exec sp_users_get_USER_PEREFERENCES_BY_USER_ID @UserID --GetUserPrefferences
exec sp_users_GET_USER_SECTIONS_BY_USER_ID @UserID --GetUserSection
exec sp_roles_get_SQL_GET_RIGHTS -- GetALLRights
exec sp_WorkFlowPreferences_get_workflow_bySiteID @SiteID --GetWorkFlowPrefferences
exec sp_SectionFlags_Get_Section_Flags_Table @CompanyID -- GetSectionFlags
exec sp_taxrates_get_tax --GetTaxRates

select tbl_markup.MarkUpID,tbl_markup.MarkUpName,
tbl_markup.MarkUpRate,tbl_markup.SystemSiteID,tbl_markup.IsFixed from tbl_markup 
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_markup.SystemSiteID) 
where tbl_company_sites.companysiteid in (@SiteID)
order by tbl_markup.MarkUpRate -- GetMarkups

Declare @Region Varchar(30)
Set @Region = (select region from tbl_general_settings where companyid =@CompanyID)
exec sp_papersizes_get_papersizetable @Region -- GetPaperSizesRegionBased
exec sp_generalsettings_get @CompanyID -- GetGeneralSetiingofCompany

SELECT tbl_contactcompanies.ContactCompanyID, tbl_contactcompanies.Name 
FROM tbl_contactcompanies 
INNER JOIN tbl_company_sites ON tbl_contactcompanies.SystemSiteID = tbl_company_sites.CompanySiteID
WHERE (tbl_contactcompanies.IsGeneral <> 0) and tbl_contactcompanies.SystemSiteID= @SiteID --GetGeneralCustomer

exec sp_ChartOfAccounts_get_CONTROLACCOUNTS @SiteID --GetDafaultNominals

exec sp_Scheduling_get_SchedulingPrefrances @UserID -- GetSchedulingPrefferences
	
END