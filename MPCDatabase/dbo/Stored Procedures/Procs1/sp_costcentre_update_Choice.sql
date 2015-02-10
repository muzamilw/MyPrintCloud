CREATE PROCEDURE dbo.sp_costcentre_update_Choice
(
@Choice varchar(255),
@InstructionID int,
@ID int
)
AS

update tbl_costcentre_workinstructions_choices set Choice=@Choice,InstructionID=@InstructionID where ID=@ID

RETURN