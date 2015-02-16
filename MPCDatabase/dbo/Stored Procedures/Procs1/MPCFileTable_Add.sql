
CREATE PROCEDURE [dbo].[MPCFileTable_Add] (@filename nvarchar(255),@filedata varbinary(max), @pathlocator nvarchar(255), 
	@isdirectory bit, @fileTableName nvarchar(100))
    AS
	BEGIN
	DECLARE @docid uniqueidentifier;
	DECLARE @newpath nvarchar(MAX);
	
        SET @docid = NEWID();
		SET @newpath = [dbo].[GetMPCFileTableNewPathLocator] (@pathlocator, @fileTableName)
		IF @fileTableName = 'Artwork'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.ArtworkFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.ArtworkFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.ArtworkFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Attachment'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.AttachmentFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.AttachmentFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.AttachmentFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Category'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.CategoryFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.CategoryFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.CategoryFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'CompanyBanner'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.CompanyBannerFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.CompanyBannerFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.CompanyBannerFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'CostCentre'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.CostCentreFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.CostCentreFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.CostCentreFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Media'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.MediaFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.MediaFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.MediaFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Organisation'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.OrganisationFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.OrganisationFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.OrganisationFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Product'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.ProductFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.ProductFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.ProductFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'SecondaryPage'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.SecondaryPageFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.SecondaryPageFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.SecondaryPageFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Store'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.StoreFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.StoreFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END

			Select stream_id, file_stream.PathName() as unc_path FROM dbo.StoreFileTable where stream_id = @docid
		END
		ELSE IF @fileTableName = 'Template'
		BEGIN
			IF @newpath IS NOT NULL
			BEGIN
				INSERT INTO dbo.TemplateFileTable(stream_id, file_stream, name, path_locator, is_directory) 
				VALUES (@docId ,@filedata, @filename, @newpath, @isdirectory)
			END
			ELSE
			BEGIN
				INSERT INTO dbo.TemplateFileTable(stream_id, name, is_directory) 
				VALUES (@docid, @filename, @isdirectory)
			END
			
			Select stream_id, file_stream.PathName() as unc_path FROM dbo.TemplateFileTable where stream_id = @docid
		END
			
    END