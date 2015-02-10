CREATE PROCEDURE dbo.sp_costcentrequestion_insert
(@SystemSiteID int,
@QuestionString varchar(150),
@QuestionType int,
@DefaultAnswer varchar(100))
AS
insert into tbl_costcentrequestions (SystemSiteID,QuestionString,Type,DefaultAnswer) VALUES 
(@SystemSiteID,@QuestionString,@QuestionType,@DefaultAnswer);
Select @@Identity
                RETURN