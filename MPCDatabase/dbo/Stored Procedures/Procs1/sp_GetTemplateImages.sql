


-- =============================================
-- Author:		Saqib Ali
-- Create date: 1 Jan 2014
-- Description:	Getting Images for DAM with paging
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetTemplateImages]
	@isCalledFrom int = 0, 
	@imageSetType int = 0, 
	@templateID bigint = 0, 
	@contactCompanyID bigint = 0, 
	@contactID bigint = 0, 
	@territory bigint = 0, 
	@pageNumber int = 0,
	@pageSize int =0,
	@sortColumn nvarchar = '',
	@search varchar(255) = '',
	@imageCount int = 0 output 
AS

BEGIN
	SET NOCOUNT ON;
	declare @result TABLE (			ID int,
	ProductID int,
	ImageName varchar(300),
	Name varchar(300),
	flgPhotobook bit,
	flgCover bit,
	BackgroundImageAbsolutePath nvarchar(500),
	BackgroundImageRelativePath nvarchar(500),
	ImageType int,
	ImageWidth int,
	ImageHeight int,
	ImageTitle nvarchar(max),
	ImageDescription nvarchar(max),
	ImageKeywords nvarchar(max),
	UploadedFrom int,
	ContactCompanyID int,
	ContactID int) 
	IF(@isCalledFrom = 1)  --DESIGNER V2
		BEGIN 
			-- getting old template images and all the images uploaded by DESIGNERS
			IF(@contactCompanyID = -999) 
				BEGIN
					IF(@imageSetType = 1) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 12) 
						BEGIN
						Insert into @result
							select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 6 ) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 1 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
					ELSE IF ( @imageSetType = 7) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 3 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
					ELSE IF ( @imageSetType = 13) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 13 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
					ELSE IF ( @imageSetType = 14) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 14 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
				END
			ELSE
				BEGIN
					-- DESINGER V2 CUSTOMER MODE
					IF(@imageSetType = 1) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF(@imageSetType = 12) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 10) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 1 and  ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
					ELSE IF (@imageSetType = 11) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where (imagetype = 3 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								ORDER BY  ID DESC
						END
				END
		END
	ELSE IF(@isCalledFrom = 2)  --mis
		BEGIN 
		-- getting old template images and all the images uploaded by that Company
			IF(@imageSetType = 1) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF(@imageSetType = 12) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF (@imageSetType = 2) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 1 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF (@imageSetType = 3) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 3 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END
			ELSE IF (@imageSetType = 16) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 16 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END	 
			ELSE IF (@imageSetType = 17) 
				BEGIN
					Insert into @result
						select * from templateBackgroundImage where (imagetype = 17 and ContactCompanyID = @contactCompanyID and (ContactID = 0 or ContactID is null)) 
						and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
						ORDER BY  ID DESC
				END	
		END
	ELSE IF (@isCalledFrom = 3) --retail end user
	BEGIN
	   -- -999 is contact company id of designerv2 mpc users 
		IF(@imageSetType = 1) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF(@imageSetType = 12) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 6 ) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where  imagetype = 1 and ContactCompanyID = -999 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
				    ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 7) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where imagetype = 3 and  ContactCompanyID = -999 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
				    ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 8) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where ( imagetype = 1 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
					ORDER BY  ID DESC
			END		
		ELSE IF (@imageSetType = 9 ) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where ( imagetype = 3 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
					ORDER BY  ID DESC
			END		
		ELSE IF (@imageSetType = 15 ) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where ( imagetype = 14 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
					ORDER BY  ID DESC
			END	
		ELSE IF ( @imageSetType = 13) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 13 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END
		ELSE IF ( @imageSetType = 14) 
						BEGIN
							Insert into @result
								select * from templateBackgroundImage where ( imagetype = 14 and ContactCompanyID = -999)
								and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%')
								 ORDER BY  ID DESC
						END		
	END
	ELSE IF (@isCalledFrom = 4)  -- mis end user
	BEGIN
		IF(@imageSetType = 1) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 2 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF(@imageSetType = 12) 
			BEGIN
				Insert into @result
					select * from templateBackgroundImage where (imagetype = 4 and ProductID =  @templateID) 
					and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
					ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 2) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 1 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 3) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 3 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END
		ELSE IF (@imageSetType = 4) 
			BEGIN
				Insert into @result
				select * from templateBackgroundImage where (imagetype = 1 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
				and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
				ORDER BY  ID DESC
			END		
		ELSE IF (@imageSetType = 5) 
			BEGIN
				Insert into @result
				select * from templateBackgroundImage where (imagetype = 3 and ContactCompanyID = @contactCompanyID and ContactID =@contactID) 
				and  (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%') 
				ORDER BY  ID DESC
			END	
		ELSE IF (@imageSetType = 16) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 16 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END	
		ELSE IF (@imageSetType = 17) 
			BEGIN
				Insert into @result	
				 select tbi.* from templateBackgroundImage tbi
				 inner join ImagePermissions ip on ip.ImageID = tbi.ID
				 where ip.TerritoryID = @territory 
				 and  (imagetype = 17 and (@search = '' or ImageTitle like '%'+@search+'%' or imageDescription like '%'+@search+'%' or imageKeywords like '%'+@search+'%'))
				 ORDER BY  ID DESC
			END
	END
	
	select @imageCount = count(ID) from @result
	-- apply sort by column name
	--SELECT * FROM @result
		--ORDER BY @sortColumn;
	-- result selected now apply paging
	declare @currPage int = 1
   
	declare @RecCount int = 0
	select @recCount = count(ID) from @result
	declare @Start int = 1
	declare @end int = 10

	if(@pageNumber != 1)
	   Begin
		set @start = (@pageNumber * @pageSize) - @pageSize + 1
	   End
	else
	   Begin 
		 set @start = 1
	   End

	set  @end = @Start + @pageSize
 
	Select * From
	(SELECT ROW_NUMBER() OVER(ORDER BY ID desc) AS RowNum, *
	From @result) as sub
	Where sub.RowNum >= @Start and sub.RowNum < @end
	
	
END