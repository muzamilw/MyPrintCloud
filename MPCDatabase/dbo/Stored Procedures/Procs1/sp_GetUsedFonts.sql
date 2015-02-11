-- =============================================
-- Author:		Muzz
-- Create date: 
-- Description:	
-- [sp_GetUsedFonts] 1616
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetUsedFonts] 
	-- Add the parameters for the stored procedure here
	@productID int = 0 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    select *, null as FontBytes from (
	SELECT [ProductFontId]
      ,[ProductId]
      ,[FontName]
      ,[FontDisplayName]
      ,[FontFile]
      ,[DisplayIndex]
      ,[IsPrivateFont]
      ,[IsEnable]
      ,[CustomerID]
	  ,[FontPath]
		FROM [dbo].[TemplateFonts]
		where isprivatefont = 0
	union
		SELECT [ProductFontId]
		  ,[ProductId]
		  ,[FontName]
		  ,[FontDisplayName]
		  ,[FontFile]
		  ,[DisplayIndex]
		  ,[IsPrivateFont]
		  ,[IsEnable] 
		  ,[CustomerID]
		  ,[FontPath]
		  from templatefonts
		where fontname in (

		select fontname from dbo.TemplateObjects
		where productid = @productID)
		) Templ
END