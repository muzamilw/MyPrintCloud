
create PROCEDURE [dbo].[sp_costcentre_update_systemCostcentre]
(
@ID int,
@IsScheduleable bit,
@IsDirectCost bit,
@Description text,
@LastModifiedBy int,
@MinimumCost float,
@DefaultVA int,
@NominalCode int
)
AS
update tbl_costcentres set IsScheduleable=@IsScheduleable,IsDirectCost=@IsDirectCost,Description=@Description,
LastModifiedBy=@LastModifiedBy,MinimumCost=@MinimumCost,DefaultVAID=@DefaultVA,nominalCode=@NominalCode 
where CostCentreID=@ID
         RETURN