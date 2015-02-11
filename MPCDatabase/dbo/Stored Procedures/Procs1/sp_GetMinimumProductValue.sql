-- =============================================
-- Author:		Khurram
-- Create date: 12/01/2015
-- Description:	To Get Minimum Product value
-- =============================================
CREATE PROCEDURE sp_GetMinimumProductValue
	-- Add the parameters for the stored procedure here
	@itemId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @value float;

    -- Insert statements for procedure here
	exec @value = dbo.funGetMiniumProductValue @itemId;
	SELECT @value MinPrice;
END