CREATE PROCEDURE dbo.sp_costcentrequestion_delete
(@QuestionID int
)
AS
delete from tbl_costcentrequestions where Id=@QuestionID;
delete from tbl_costcentreanswers where QuestionID=@QuestionID
               RETURN