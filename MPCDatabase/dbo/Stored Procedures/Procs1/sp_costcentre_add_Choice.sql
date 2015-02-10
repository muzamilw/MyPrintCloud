CREATE PROCEDURE dbo.sp_costcentre_add_Choice
(
@Choice varchar(255),
@InstructionID int
)
AS

insert into tbl_costcentre_workinstructions_choices values (@Choice,@InstructionID)

Select @@Identity
RETURN