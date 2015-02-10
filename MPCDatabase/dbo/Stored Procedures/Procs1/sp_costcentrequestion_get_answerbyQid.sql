CREATE PROCEDURE dbo.sp_costcentrequestion_get_answerbyQid
(@QuestionID int
)
AS
select * from tbl_costcentreanswers where QuestionID=@QuestionID 
               RETURN