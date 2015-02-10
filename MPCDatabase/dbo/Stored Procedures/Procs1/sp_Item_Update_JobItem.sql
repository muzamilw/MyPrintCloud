--- The Sp is only for Item 

CREATE PROCEDURE dbo.sp_Item_Update_JobItem

	(
		
@JobDescriptionTitle1 as text,
@JobDescriptionTitle2 as text,
@JobDescriptionTitle3 as text, 
@JobDescriptionTitle4 as text,
@JobDescriptionTitle5 as text,
@JobDescriptionTitle6 as text,
@JobDescriptionTitle7 as text,
@JobDescriptionTitle8 as text,
@JobDescriptionTitle9 as text,
@JobDescriptionTitle10 as text,
@JobDescription1 as text,
@JobDescription2 as text,
@JobDescription3 as text,
@JobDescription4 as text,
@JobDescription5 as text,
@JobDescription6 as text,
@JobDescription7 as text,
@JobDescription8 as text,
@JobDescription9 as text,
@JobDescription10 as text,
@JobDescription as text,
--@JobManagerID as int,
@JobStatusID as int,
@ItemID int

	)

AS
	
	update tbl_items set 
				JobDescriptionTitle1= @JobDescriptionTitle1,
				JobDescriptionTitle2 =@JobDescriptionTitle2 ,
				JobDescriptionTitle3 = @JobDescriptionTitle3 , 
				JobDescriptionTitle4 = @JobDescriptionTitle4 ,
				JobDescriptionTitle5 = @JobDescriptionTitle5 ,
				JobDescriptionTitle6 = @JobDescriptionTitle6 ,
				JobDescriptionTitle7 = @JobDescriptionTitle7 ,
				JobDescriptionTitle8 = @JobDescriptionTitle8 ,
				JobDescriptionTitle9 = @JobDescriptionTitle9 ,
				JobDescriptionTitle10 = @JobDescriptionTitle10 ,
				JobDescription1 = @JobDescription1 ,
				JobDescription2 = @JobDescription2 ,
				JobDescription3 = @JobDescription3 ,
				JobDescription4 = @JobDescription4 ,
				JobDescription5 = @JobDescription5 ,
				JobDescription6 = @JobDescription6 ,
				JobDescription7 = @JobDescription7 ,
				JobDescription8 = @JobDescription8 ,
				JobDescription9 = @JobDescription9 ,
				JobDescription10 = @JobDescription10 ,
				JobDescription = @JobDescription ,
				--JobManagerID = @JobManagerID ,
				JobStatusID = @JobStatusID 	
	       where ItemID=@ItemID
	RETURN