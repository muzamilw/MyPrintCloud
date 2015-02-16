CREATE PROCEDURE dbo.sp_pagination_get_finishingstylebyID
(
@ID int)
AS
	SELECT tbl_pagination_finishstyle.ID,tbl_pagination_finishstyle.Name,
         tbl_pagination_finishstyle.Head,tbl_pagination_finishstyle.Trim,tbl_pagination_finishstyle.Foredge,
         tbl_pagination_finishstyle.Spine FROM tbl_pagination_finishstyle where ID=@ID
                RETURN