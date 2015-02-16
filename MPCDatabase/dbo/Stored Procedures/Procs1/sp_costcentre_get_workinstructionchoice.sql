CREATE PROCEDURE dbo.sp_costcentre_get_workinstructionchoice
(@ID int)
AS
SELECT tbl_costcentre_workinstructions_choices.ID, 
        tbl_costcentre_workinstructions_choices.Choice,tbl_costcentre_workinstructions_choices.InstructionID 
        FROM tbl_costcentre_instructions 
        INNER JOIN tbl_costcentre_workinstructions_choices ON (tbl_costcentre_instructions.InstructionID = tbl_costcentre_workinstructions_choices.InstructionID) 
        WHERE tbl_costcentre_instructions.CostCentreID = @ID
         RETURN