CREATE PROCEDURE dbo.sp_jobcard_get_by_SiteID
(
@SystemSiteID int)
                  AS
SELECT tbl_job_preferences.ID,tbl_job_preferences.IsDefaultInkColorUsed,tbl_job_preferences.IsDefaultFilmUsed,tbl_job_preferences.IsdefaultPlateUsed,tbl_job_preferences.IsDefaultMakereadyUsed,tbl_job_preferences.IsDefaultWashupUsed,
         tbl_job_preferences.IsDefaultPressInstruction,tbl_job_preferences.IsWorkingSize,tbl_job_preferences.IsItemSize,tbl_job_preferences.IsImpressionCount,tbl_job_preferences.IsNumberOfPasses,tbl_job_preferences.IsPrintSheetQty,tbl_job_preferences.IsDefaultStockDetail,tbl_job_preferences.IsOrderSheetSize,tbl_job_preferences.IsSpoilageAllowed,
         tbl_job_preferences.IsPaperWeight,tbl_job_preferences.IsPaperSheetQty,tbl_job_preferences.IsDefaultGuilotine,tbl_job_preferences.IsGuilotineWorkingSize,tbl_job_preferences.IsGuilotineItemSize,tbl_job_preferences.IsNoOfTrims,tbl_job_preferences.IsNoOfCuts, IsInksEstTime, InFilmEstTime, IsPlateEstTime, IsWashupEstTime, IsMakereadyEstTime, IsPressEstTime, IsPaperEstTime, IsGuillotineEstTime,IsReelMakeReady ,IsReelMakeReadyTime 
          FROM tbl_job_preferences 
         WHERE tbl_job_preferences.SystemSiteID = @SystemSiteID
                     RETURN