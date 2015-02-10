CREATE PROCEDURE dbo.sp_pagination_insert
(@SystemSiteID int,
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
@NoOfDifferentTypes int,
@FlagID int)
AS
	insert into tbl_pagination_profile (SystemSiteID,Code,Description,Priority,Pages,PaperSizeID,LookupMethodID,Orientation,FinishStyleID,MinHeight,Minwidth,Maxheight,MaxWidth,MinWeight,MaxWeight,MaxNoOfColors,GrainDirection,NumberUp,NoOfDifferentTypes,FlagID) VALUES 
        (@SystemSiteID,@Code,@Description,@Priority,@Pages,@PaperSizeID,@LookupMethodID,@Orientation,@FinishStyleID,@MinHeight,@Minwidth,@Maxheight,@MaxWidth,@MinWeight,@MaxWeight,@MaxNoOfColors,@GrainDirection,@NumberUp,@NoOfDifferentTypes,@FlagID);
        Select @@Identity as PaginationID
	RETURN