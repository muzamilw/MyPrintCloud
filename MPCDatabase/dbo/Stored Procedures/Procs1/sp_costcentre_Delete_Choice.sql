CREATE PROCEDURE dbo.sp_costcentre_Delete_Choice
(
@ID int
)
AS

delete from tbl_costcentre_workinstructions_choices where ID=@ID

RETURN