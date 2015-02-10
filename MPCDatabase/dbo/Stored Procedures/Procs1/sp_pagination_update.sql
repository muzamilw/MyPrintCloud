CREATE PROCEDURE dbo.sp_pagination_update
(
@ID int,
@Code varchar(50),
@Description text,
@Priority int,
@Pages int,
@PaperSizeID int,
@LookupMethodID int,
@Orientation int,
@FinishStyleID int,
@MinHeight float,
@Minwidth float,
@Maxheight float,
@MaxWidth float,
@MinWeight float,
@MaxWeight float,
@MaxNoOfColors int,
@GrainDirection varchar(50),
@NumberUp int,
@NoOfDifferentTypes int)
AS
	update tbl_pagination_profile set Code=@Code,Description=@Description,Priority=@Priority,Pages=@Pages,PaperSizeID=@PaperSizeID,LookupMethodID=@LookupMethodID,Orientation=@Orientation,FinishStyleID=@FinishStyleID,MinHeight=@MinHeight,Minwidth=@Minwidth,Maxheight=@Maxheight,MaxWidth=@MaxWidth,MinWeight=@MinWeight,MaxWeight=@MaxWeight,MaxNoOfColors=@MaxNoOfColors,GrainDirection=@GrainDirection,NumberUp=@NumberUp,NoOfDifferentTypes=@NoOfDifferentTypes where ID=@ID
	RETURN