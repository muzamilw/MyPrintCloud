CREATE PROCEDURE dbo.sp_pagination_get_Filter_PaginationByCombinationDetails
(
@Pages int,
@Height float,
@Width float,
@UniqueColors int,
@Weight float)
AS
	SELECT  tbl_machines.MachineID, tbl_machines.MachineName, tbl_machines.MachineCatID, tbl_machines.IsDisabled,  
	tbl_machine_pagination_profile.MachineID, tbl_pagination_profile.Code, tbl_pagination_profile.Description, 
	tbl_pagination_profile.Priority, tbl_pagination_profile.Pages, tbl_pagination_profile.LookupMethodID, 
	tbl_pagination_profile.PaperSizeID, tbl_pagination_profile.Orientation, tbl_pagination_profile.FinishStyleID, 
	tbl_pagination_profile.MinHeight, tbl_pagination_profile.Minwidth, tbl_pagination_profile.Maxheight, 
	tbl_pagination_profile.MaxWidth, tbl_pagination_profile.MinWeight, tbl_pagination_profile.MaxWeight, 
	tbl_pagination_profile.MaxNoOfColors FROM tbl_machines 
	INNER JOIN tbl_machine_pagination_profile ON (tbl_machines.MachineID = tbl_machine_pagination_profile.MachineID) 
	INNER JOIN tbl_pagination_profile ON (tbl_machine_pagination_profile.PaginationID = tbl_pagination_profile.ID) 
	where   tbl_pagination_profile.Pages  = @Pages and  @Height >=   tbl_pagination_profile.MinHeight 
	and @Height <=    tbl_pagination_profile.Maxheight and @Width >=   tbl_pagination_profile.Minwidth 
	and @Width <=     tbl_pagination_profile.MaxWidth and @Weight >=   tbl_pagination_profile.MinWeight 
	and @Weight <=   tbl_pagination_profile.MaxWeight and @UniqueColors <=   tbl_pagination_profile.MaxNoOfColors
                RETURN