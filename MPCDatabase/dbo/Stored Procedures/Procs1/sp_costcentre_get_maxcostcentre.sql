
create PROCEDURE [dbo].[sp_costcentre_get_maxcostcentre]

AS
select max(CostCentreID) from tbl_costcentres        
                 RETURN