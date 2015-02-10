CREATE PROCEDURE dbo.sp_pagination_get_byID
(
@ID int)
AS
	SELECT tbl_pagination_profile.ID,tbl_pagination_profile.Code,tbl_pagination_profile.FlagID,tbl_pagination_profile.Description,tbl_pagination_profile.Priority,
                 tbl_pagination_profile.Pages,tbl_pagination_profile.PaperSizeID,tbl_pagination_profile.LookupMethodID,tbl_pagination_profile.Orientation,tbl_pagination_profile.FinishStyleID,
                 tbl_pagination_profile.MinHeight,tbl_pagination_profile.Minwidth,tbl_pagination_profile.Maxheight,tbl_pagination_profile.MaxWidth,tbl_pagination_profile.MinWeight,
                 tbl_pagination_profile.MaxWeight,tbl_pagination_profile.MaxNoOfColors,tbl_pagination_profile.GrainDirection,tbl_pagination_profile.NumberUp,tbl_pagination_profile.NoOfDifferentTypes 
                 FROM tbl_pagination_profile where ID=@ID
                RETURN