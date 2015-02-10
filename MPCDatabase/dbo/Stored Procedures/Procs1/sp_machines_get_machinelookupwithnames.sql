CREATE PROCEDURE dbo.sp_machines_get_machinelookupwithnames
(@MachineID int)
AS
	SELECT tbl_machine_lookup_methods.ID, 
         tbl_machine_lookup_methods.MachineID,tbl_machine_lookup_methods.MethodID,tbl_machine_lookup_methods.DefaultMethod,
         tbl_lookup_methods.Name,tbl_lookup_methods.Type FROM tbl_lookup_methods 
         INNER JOIN tbl_machine_lookup_methods ON (tbl_lookup_methods.MethodID = tbl_machine_lookup_methods.MethodID) 
         WHERE tbl_machine_lookup_methods.MachineID = @MachineID
        RETURN