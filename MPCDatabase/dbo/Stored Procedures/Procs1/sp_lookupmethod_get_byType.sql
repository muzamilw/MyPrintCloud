CREATE PROCEDURE dbo.sp_lookupmethod_get_byType
(@MethodID int)
AS
SELECT tbl_lookup_methods.Name,tbl_lookup_methods.Type,
		(
		case
		when  tbl_machine_clickchargezone.ID>0
			then	tbl_machine_clickchargezone.ID 
		when tbl_machine_guillotinecalc.ID>0
			then	tbl_machine_guillotinecalc.ID 
		when tbl_machine_clickchargelookup.ID>0
			then	tbl_machine_clickchargelookup.ID
		when tbl_machine_perhourlookup.ID>0
			then	tbl_machine_perhourlookup.ID 
		when tbl_machine_speedweightlookup.ID>0
			then	tbl_machine_speedweightlookup.ID  
		else
			0		
		end	)
       as LookupID,tbl_lookup_methods.MethodID 
        
         FROM tbl_machine_speedweightlookup 
         RIGHT OUTER JOIN tbl_lookup_methods ON (tbl_machine_speedweightlookup.MethodID = tbl_lookup_methods.MethodID) 
         LEFT OUTER JOIN tbl_machine_clickchargezone ON (tbl_lookup_methods.MethodID = tbl_machine_clickchargezone.MethodID) 
         LEFT OUTER JOIN tbl_machine_guillotinecalc ON (tbl_lookup_methods.MethodID = tbl_machine_guillotinecalc.MethodID) 
         LEFT OUTER JOIN tbl_machine_clickchargelookup ON (tbl_lookup_methods.MethodID = tbl_machine_clickchargelookup.MethodID) 
         LEFT OUTER JOIN tbl_machine_perhourlookup ON (tbl_lookup_methods.MethodID = tbl_machine_perhourlookup.MethodID) where tbl_lookup_methods.MethodID =@MethodID        
                 RETURN