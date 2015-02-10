CREATE PROCEDURE [dbo].[sp_ItemAttachments_Get_ByAttachmentID]
(
	@ID int
)
AS
	SELECT  tbl_item_attachments.ID,tbl_item_attachments.FileTitle,tbl_item_attachments.isFromCustomer, 
        tbl_item_attachments.CustomerID,tbl_item_attachments.ContactID,tbl_item_attachments.SystemUserID,tbl_item_attachments.UploadDate,
        tbl_item_attachments.UploadTime,tbl_item_attachments.Version,tbl_item_attachments.ItemID,tbl_item_attachments.Comments,tbl_item_attachments.Type,
        tbl_item_attachments.IsApproved,tbl_item_attachments.FileName,tbl_item_attachments.ApproveDate,tbl_item_attachments.FolderPath,tbl_item_attachments.FileType,tbl_item_attachments.ContentType,
        tbl_item_attachments.Parent,(case tbl_item_attachments.isFromCustomer when 0 then tbl_systemusers.UserName else tbl_contactCompanies.name end) as FromName
        FROM tbl_item_attachments 
        LEFT OUTER JOIN tbl_systemusers ON (tbl_item_attachments.SystemUserID = tbl_systemusers.SystemUserID) 
        LEFT OUTER JOIN tbl_contactCompanies ON (tbl_item_attachments.CustomerID = tbl_contactCompanies.ContactCompanyID) 
        WHERE ID = @ID order by tbl_item_attachments.version desc
	RETURN