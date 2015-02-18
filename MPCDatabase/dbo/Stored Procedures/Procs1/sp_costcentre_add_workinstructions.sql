
CREATE PROCEDURE [dbo].[sp_costcentre_add_workinstructions]
(
@CostCentreID int,
@Instruction varchar(255)
)
AS

insert into tbl_costcentre_instructions values (@Instruction,@CostCentreID, NULL)
Select @@Identity

         RETURN