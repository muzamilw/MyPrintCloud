CREATE PROCEDURE dbo.sp_phrasebook_get_fieldsbysections
(@SectionID int)
AS
	select FieldID,FieldName from tbl_phrase_fields where SectionID=@SectionID
	RETURN