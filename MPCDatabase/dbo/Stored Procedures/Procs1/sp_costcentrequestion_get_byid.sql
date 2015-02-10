CREATE PROCEDURE dbo.sp_costcentrequestion_get_byid
(@QuestionID int
)
AS
Select * from tbl_costcentrequestions where ID=@QuestionID 
               RETURN