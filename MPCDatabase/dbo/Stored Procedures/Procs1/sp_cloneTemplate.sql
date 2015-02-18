----------------------------------------- Alter Store Procedure sp_cloneTemplate ----------------------
CREATE PROCEDURE [dbo].[sp_cloneTemplate] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint,
	@submittedBy bigint,
    @submittedByName nvarchar(100)
AS
BEGIN


      
	declare @NewTemplateID bigint
	declare @NewCode nvarchar(10)
	--DECLARE  @WaterMarkTxt as [dbo].[tbl_company_sites]
	
	set @NewCode = ''
	
	--INSERT INTO @WaterMarkTxt (CompanySiteName)
	--Select Top 1 CompanySiteName from tbl_company_sites

	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[Template]
           ([Code]
           ,[ProductName]
           ,[Description]
           ,[ProductCategoryId]
           ,[Thumbnail]
           ,[Image]
           ,[IsDisabled]

           ,[PDFTemplateWidth]
           ,[PDFTemplateHeight]

           ,[CuttingMargin]
           ,[MultiPageCount]
           ,[Orientation]
           ,[MatchingSetTheme]
           ,[BaseColorId]
           ,[SubmittedBy]
           ,[SubmittedByName]
           ,[SubmitDate]
           ,[Status]
           ,[ApprovedBy]
           ,[ApprovedByName]
           ,[UserRating]
           ,[UsedCount]
           ,[MPCRating]
           ,[RejectionReason]
           ,[ApprovalDate]
           ,[IsCorporateEditable]
           ,[MatchingSetId]
           ,[TempString],[TemplateType],[isSpotTemplate]
          )
     



   
	SELECT 
      @NewCode
      ,[ProductName] + ' Copy'
      ,[Description]
      ,[ProductCategoryID]
      
      ,[Thumbnail]
      ,[Image]
      ,[IsDisabled]

      ,[PDFTemplateWidth]
      ,[PDFTemplateHeight]


      ,[CuttingMargin]
      ,[MultiPageCount]
      ,[Orientation]
      ,[MatchingSetTheme]
      ,[BaseColorId]
      ,@submittedBy
      ,@submittedByName
      ,NULL
      ,1
      ,NULL
      ,NULL
      ,0
      ,0
      ,0
      ,''
      ,NULL,IsCorporateEditable,MatchingSetId,'',[TemplateType],isSpotTemplate
      
  FROM [dbo].[Template] where productid = @TemplateID
	
	
	set @NewTemplateID = SCOPE_IDENTITY() 
	
	-- updating water mark text 
	UPDATE [dbo].[Template]
	SET TempString= (Select Top 1 OrganisationName from organisation)
	WHERE productid = @NewTemplateID
	
	--copying the pages
	INSERT INTO [dbo].[TemplatePage]
           ([ProductId]
           ,[PageNo]
           ,[PageType]
           ,[Orientation]
           ,[BackGroundType]
           ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
           ,[PageName],[hasOverlayObjects])

SELECT 
      @NewTemplateID
      ,[PageNo]
      ,[PageType]
      ,[Orientation]
      ,[BackGroundType]
      ,[BackgroundFileName]
      ,[ColorC]
      ,[ColorM]
      ,[ColorY]
      ,[ColorK]
      ,[IsPrintable]
      
      ,[PageName],[hasOverlayObjects]
  FROM [dbo].[TemplatePage]
where productid = @TemplateID


	--copying the objects
	INSERT INTO [dbo].[TemplateObject]
           ([ObjectType]
           ,[Name]
           ,[IsEditable]
           ,[IsHidden]
           ,[IsMandatory]
           
           ,[PositionX]
           ,[PositionY]
           ,[MaxHeight]
           ,[MaxWidth]
           ,[MaxCharacters]
           ,[RotationAngle]
           ,[IsFontCustom]
           ,[IsFontNamePrivate]
           ,[FontName]
           ,[FontSize]
           ,[IsBold]
           ,[IsItalic]
           ,[Allignment]
           ,[VAllignment]
           ,[Indent]
           ,[IsUnderlinedText]
           ,[ColorType]
 
           ,[ColorName]
           ,[ColorC]
           ,[ColorM]
           ,[ColorY]
           ,[ColorK]
           ,[Tint]
           ,[IsSpotColor]
           ,[SpotColorName]
           ,[ContentString]
           ,[ContentCaseType]
           ,[ProductID]
           ,[DisplayOrderPdf]
           ,[DisplayOrderTxtControl]
           ,[RColor]
           ,[GColor]
           ,[BColor]
           ,[LineSpacing]
           ,[ProductPageId]
           ,[ParentId]
           ,CircleRadiusX
           ,Opacity
           ,[ExField1],
           [IsTextEditable],
           [IsPositionLocked],
           [CircleRadiusY]
          ,[ExField2],
           ColorHex
           ,[IsQuickText]
           ,[QuickTextOrder],[watermarkText],[textStyles],[charspacing],[AutoShrinkText],[IsOverlayObject],[ClippedInfo]
           )
	SELECT 
      O.[ObjectType]
      ,O.[Name]
      ,O.[IsEditable]
      ,O.[IsHidden]
      ,O.[IsMandatory]
      
      ,O.[PositionX]
      ,O.[PositionY]
      ,O.[MaxHeight]
      ,O.[MaxWidth]
      ,O.[MaxCharacters]
      ,O.[RotationAngle]
      ,O.[IsFontCustom]
      ,O.[IsFontNamePrivate]
      ,O.[FontName]
      ,O.[FontSize]
      ,O.[IsBold]
      ,O.[IsItalic]
      ,O.[Allignment]
      ,O.[VAllignment]
      ,O.[Indent]
      ,O.[IsUnderlinedText]
      ,O.[ColorType]
      ,O.[ColorName]
      ,O.[ColorC]
      ,O.[ColorM]
      ,O.[ColorY]
      ,O.[ColorK]
      ,O.[Tint]
      ,O.[IsSpotColor]
      ,O.[SpotColorName]
      ,O.[ContentString]
      ,O.[ContentCaseType]
      ,@NewTemplateID
      ,O.[DisplayOrderPdf]
      ,O.[DisplayOrderTxtControl]
      ,O.[RColor]
      ,O.[GColor]
      ,O.[BColor]
      ,O.[LineSpacing]
      ,NP.[ProductPageId]
      ,O.[ParentId]
      ,O.CircleRadiusX
      ,O.Opacity
      ,O.[ExField1],
       O.[IsTextEditable],
       O.[IsPositionLocked],
       O.[CircleRadiusY]
      ,O.[ExField2],
       O.ColorHex
       ,[IsQuickText]
        ,[QuickTextOrder],[watermarkText],O.[textStyles],O.[charspacing],O.[AutoShrinkText],O.[IsOverlayObject],O.[ClippedInfo]
  FROM [dbo].[TemplateObject] O
  inner join [dbo].[TemplatePage]  P on o.ProductPageId = p.ProductPageId and o.ProductId = @TemplateID
  inner join [dbo].[TemplatePage] NP on P.PageName = NP.PageName and P.PageNo = NP.PageNo and NP.ProductId = @NewTemplateID
  
	--theme tags
	--insert into dbo.TemplateThemeTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateThemeTags where ProductID = @TemplateID
	
	---- industry tags
	--insert into dbo.TemplateIndustryTags   ([TagID],[ProductID])
	--select [TagID] ,@NewTemplateID from dbo.TemplateIndustryTags where ProductID = @TemplateID

	INSERT INTO [dbo].[TemplateBackgroundImage]
			   ([ProductId]
			   ,[ImageName]
			   ,[Name]
			   ,[flgPhotobook]
			   ,[flgCover]
			   ,[BackgroundImageAbsolutePath]
			   ,[BackgroundImageRelativePath],
			   ImageType,
			   ImageWidth,
			   ImageHeight
			   
			   )
	SELECT 
		  @NewTemplateID
		  ,[ImageName]
		  ,[Name]
		  ,[flgPhotobook]
		  ,[flgCover]
		  ,[BackgroundImageAbsolutePath]
		  ,[BackgroundImageRelativePath]
		  ,ImageType,
			   ImageWidth,
			   ImageHeight
	  FROM [dbo].[TemplateBackgroundImage] where ProductId = @TemplateID




select @NewTemplateID
	
END