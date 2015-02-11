-- =============================================
-- Author:		 Saqib 
-- Create date: 08-11-2012
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetUsedFontsUpdated] 
	-- Add the parameters for the stored procedure here
	@TemplateID bigint = 0, 
	@CustomerID bigint = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--SELECT @TemplateID, @CustomerID
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
		FROM [dbo].[TemplateFont]
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
	    FROM templatefont
		where fontname in (

		select fontname from dbo.TemplateObject
		where productid = @TemplateID)
		
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
		FROM [dbo].[TemplateFont]
		where CustomerID = @CustomerID 
		) Templ
END