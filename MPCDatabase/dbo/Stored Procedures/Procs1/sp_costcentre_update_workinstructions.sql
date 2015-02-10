CREATE PROCEDURE dbo.sp_costcentre_update_workinstructions
(
@CostCentreID int,
@Instruction varchar(255),
@InstructionID int
)
AS

update tbl_costcentre_instructions set  Instruction = @Instruction ,CostCentreID=@CostCentreID where InstructionID=@InstructionID

Select @InstructionID
         RETURN