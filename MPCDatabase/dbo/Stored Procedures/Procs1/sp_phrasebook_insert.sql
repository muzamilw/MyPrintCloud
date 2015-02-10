CREATE PROCEDURE dbo.sp_phrasebook_insert
(@Phrase text,
@FieldID int,
@SystemSiteID int)
AS
	Insert into tbl_phrase (Phrase,FieldID,SystemSiteID) values (@Phrase,@FieldID,@SystemSiteID);
	select @@Identity as PhID;
	RETURN