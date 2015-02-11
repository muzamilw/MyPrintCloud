-- =============================================
-- Author:		Khurram
-- Create date: 23-12-2014
-- Description:	It generates new path locator for a given path like we want to add a file under organisation1 then 
-- if we pass '\Organisation1' as filepath then it will return pathlocator that we can set while saving record to filetable
-- =============================================
CREATE FUNCTION [dbo].[GetMPCFileTableNewPathLocator]
(
	-- Add the parameters for the function here
	@filePath nvarchar(MAX),
	@fileTableName nvarchar(100)
)
RETURNS nvarchar(MAX)
AS
BEGIN
	-- Declare the return variable here
	declare @path varchar(MAX);
	declare @new_path varchar(MAX);
	set @path =  @filePath;

	declare @parent hierarchyid;
	declare @newid uniqueidentifier;

	/* Check for filetable */
		IF @fileTableName = 'Artwork'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.ArtworkFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Attachment'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.AttachmentFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Category'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.CategoryFileTable'), @path));
		END
		ELSE IF @fileTableName = 'CompanyBanner'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.CompanyBannerFileTable'), @path));
		END
		ELSE IF @fileTableName = 'CostCentre'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.CostCentreFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Media'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.MediaFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Organisation'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.OrganisationFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Product'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.ProductFileTable'), @path));
		END
		ELSE IF @fileTableName = 'SecondaryPage'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.SecondaryPageFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Store'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.StoreFileTable'), @path));
		END
		ELSE IF @fileTableName = 'Template'
		BEGIN
			set @parent = GetPathLocator(CONCAT(FileTableRootPath('dbo.TemplateFileTable'), @path));
		END
		
	
	SELECT @newid = new_id from dbo.getNewID;
	-- Add the T-SQL statements to compute the return value here
	SELECT @new_path = @parent.ToString()     +
	CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), @newid), 1, 6))) + '.' +
	CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16), @newid), 7, 6))) + '.' +
	CONVERT(VARCHAR(20), CONVERT(BIGINT, SUBSTRING(CONVERT(BINARY(16),@newid), 13, 4))) + '/'

	-- Return the result of the function
	RETURN @new_path

END