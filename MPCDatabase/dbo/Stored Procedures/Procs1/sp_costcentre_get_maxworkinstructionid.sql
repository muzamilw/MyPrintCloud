CREATE PROCEDURE dbo.sp_costcentre_get_maxworkinstructionid

AS
		Select IDENT_CURRENT('tbl_costcentre_instructions') as SeedIndex;
         RETURN