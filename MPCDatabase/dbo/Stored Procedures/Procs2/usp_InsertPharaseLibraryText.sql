-- Created by: Naveed
-- Create Date: 2014 01 07
-- Descripton: Insert Record in Phrase Library

create PROCEDURE [dbo].[usp_InsertPharaseLibraryText] 

 @FieldID int,
 @PhraseTex text
 
AS  
BEGIN  
	 insert into tbl_phrase
	 (Phrase, FieldID, companyID, SystemSiteID)
	 Values(@PhraseTex, @FieldID, 2, 1)
END