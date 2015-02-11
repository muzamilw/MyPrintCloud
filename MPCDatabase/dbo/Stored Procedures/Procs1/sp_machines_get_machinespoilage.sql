CREATE PROCEDURE dbo.sp_machines_get_machinespoilage
(@MachineID int)
AS
	SELECT tbl_machine_spoilage.MachineSpoilageID,tbl_machine_spoilage.MachineID,
         tbl_machine_spoilage.NoOfColors,tbl_machine_spoilage.SetupSpoilage,tbl_machine_spoilage.RunningSpoilage 
         FROM tbl_machine_spoilage where tbl_machine_spoilage.MachineID=@MachineID
        RETURN