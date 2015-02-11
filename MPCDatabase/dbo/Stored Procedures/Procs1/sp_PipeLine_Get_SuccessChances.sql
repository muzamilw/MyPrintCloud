CREATE PROCEDURE dbo.sp_PipeLine_Get_SuccessChances
	
AS
	SELECT tbl_success_chance.SuccessChanceID,
       tbl_success_chance.Description,
       tbl_success_chance.Percentage 
       FROM tbl_success_chance
	RETURN