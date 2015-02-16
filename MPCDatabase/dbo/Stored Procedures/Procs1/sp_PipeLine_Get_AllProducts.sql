CREATE PROCEDURE dbo.sp_PipeLine_Get_AllProducts
	
AS
	SELECT *       
       FROM tbl_pipeline_products
	RETURN