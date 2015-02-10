
CREATE PROCEDURE [dbo].[sp_users_GET_USER_SECTIONS_BY_USER_ID]
(@SystemUserID int)
AS
	SELECT tbl_sections.SectionID, tbl_sections.SectionName, tbl_sections.SecOrder, tbl_sections.ParentID, 
	tbl_sections.href, tbl_sections.SectionImage, tbl_sections.Independent
	FROM tbl_roles 
	INNER JOIN tbl_rolesections ON (tbl_roles.RoleID = tbl_rolesections.RoleID) 
	INNER JOIN tbl_sections ON (tbl_rolesections.SectionID = tbl_sections.SectionID)
	 INNER JOIN tbl_systemusers ON (tbl_roles.RoleID = tbl_systemusers.RoleID)
	  WHERE(tbl_systemusers.SystemUserID = @SystemUserID) order by tbl_sections.SecOrder
	RETURN