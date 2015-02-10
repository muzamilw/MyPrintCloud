CREATE PROCEDURE dbo.sp_Jobs_Update_JobCard
(
	@Description1 text,
	@Description2 text,
	@Description3 text,
	@Description4 text,
	@Description5 text,
	@Description6 text,
	@Description7 text,
	@Description8 text,
	@Title1 text,
	@Title2 text,
	@Title3 text,
	@Title4 text,
	@Title5 text,
	@Title6 text,
	@Title7 text,
	@Title8 text,
	@AdditionalInformation text,
	@CompletionDate datetime,
    @ItemID int

)
AS
	update tbl_items set JobDescription1=@Description1,JobDescription2=@Description2,JobDescription3=@Description3,JobDescription4=@Description4,JobDescription5=@Description5,JobDescription6=@Description6,JobDescription7=@Description7,JobDescription8=@Description8,
        JobDescriptionTitle1=@Title1,JobDescriptionTitle2=@Title2,JobDescriptionTitle3=@Title3,JobDescriptionTitle4=@Title4,JobDescriptionTitle5=@Title5,JobDescriptionTitle6=@Title6,JobDescriptionTitle7=@Title7,JobDescriptionTitle8=@Title8, 
        AdditionalInformation=@AdditionalInformation,JobEstimatedCompletionDateTime=@CompletionDate 
        where ItemID=@ItemID 
	RETURN