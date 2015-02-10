
Create PROCEDURE [dbo].[sp_taxrates_get_tax_by_states]
(@SstateID int,
@DstateID int)
AS
	Select * from tbl_taxrate where SourceStateID=@SstateID and DestinationStateID=@DstateID order by TaxCode
	RETURN