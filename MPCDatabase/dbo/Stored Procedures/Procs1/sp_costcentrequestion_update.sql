CREATE PROCEDURE dbo.sp_costcentrequestion_update
(
@QuestionString varchar(150),
@QuestionType int,
@DefaultAnswer varchar(100),@QuestionID int
)
AS
update tbl_costcentrequestions set QuestionString=@QuestionString,Type=@QuestionType,DefaultAnswer=@DefaultAnswer where Id=@QuestionID
                RETURN