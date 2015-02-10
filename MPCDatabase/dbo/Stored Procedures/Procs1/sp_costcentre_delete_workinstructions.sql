CREATE PROCEDURE dbo.sp_costcentre_delete_workinstructions
(
@InstructionID int
)
AS

Delete from tbl_costcentre_instructions where InstructionID=@InstructionID

RETURN