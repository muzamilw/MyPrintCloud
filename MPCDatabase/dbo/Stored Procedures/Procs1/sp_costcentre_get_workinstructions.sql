CREATE PROCEDURE dbo.sp_costcentre_get_workinstructions
(
@ID int
)
AS
SELECT tbl_costcentre_instructions.InstructionID, tbl_costcentre_instructions.Instruction, tbl_costcentre_instructions.CostCentreID FROM tbl_costcentre_instructions where tbl_costcentre_instructions.CostcentreID=@ID
         RETURN