
CREATE PROCEDURE [dbo].[MPCFileTable_Del] (@docId uniqueidentifier, @fileTableName nvarchar(100))
    AS
    BEGIN
        SET NOCOUNT ON;

	/* Check for filetable */
		IF @fileTableName = 'Artwork'
		BEGIN
			DELETE from ArtworkFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Attachment'
		BEGIN
			DELETE from AttachmentFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Category'
		BEGIN
			DELETE from CategoryFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'CompanyBanner'
		BEGIN
			DELETE from CompanyBannerFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'CostCentre'
		BEGIN
			DELETE from CostCentreFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Media'
		BEGIN
			DELETE from MediaFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Organisation'
		BEGIN
			DELETE from OrganisationFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Product'
		BEGIN
			DELETE from ProductFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'SecondaryPage'
		BEGIN
			DELETE from SecondaryPageFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Store'
		BEGIN
			DELETE from StoreFileTable where stream_id = @docId;
		END
		ELSE IF @fileTableName = 'Template'
		BEGIN
			DELETE from TemplateFileTable where stream_id = @docId;
		END
           
    END