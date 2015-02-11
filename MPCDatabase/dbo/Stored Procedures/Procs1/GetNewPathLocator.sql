-- =============================================
-- Author:		Khurram
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetNewPathLocator]
	-- Add the parameters for the stored procedure here
	@filePath nvarchar(MAX), @fileTableName nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @new_id nvarchar(MAX);
    -- Insert statements for procedure here
	SET @new_id = dbo.GetMPCFileTableNewPathLocator(@filePath, @fileTableName)
	SELECT @new_id
END