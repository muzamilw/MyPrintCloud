
CREATE PROCEDURE [dbo].[sp_costcentre_check_name]
(@CostCentreID int,@Name varchar(100),@SiteID int)
AS
select CostCentreID from tbl_costcentres where (CostCentreID<>@CostCentreID and Name=@Name) and SystemSiteID=@SiteID
         RETURN