﻿CREATE PROCEDURE dbo.sp_ItemSectionCostCentre_Update_ItemSectionCostCentrePurchaseOrderStatus
(
@SectionCostcentreID int

)
AS
	update tbl_section_costcentres set IsPurchaseOrderRaised=0 where SectionCostcentreID=@SectionCostcentreID
	RETURN