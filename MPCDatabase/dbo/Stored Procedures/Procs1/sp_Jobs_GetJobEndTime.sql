
Create PROCEDURE [dbo].[sp_Jobs_GetJobEndTime]
(
		@ItemID int,
		@JobStartDate datetime
	)

AS

DECLARE @CostCenterStartTime datetime 
Declare @CostCenterID int
Declare @CostCenterDuration float

set @CostCenterStartTime =@JobStartDate


DECLARE CostCenterTime_cursor CURSOR FOR

SELECT tbl_section_costcentres.SectionCostcentreID,

case tbl_Items.jobSelectedQty

when 1 then
tbl_section_costcentres.Qty1EstimatedTime
when 2 then 
tbl_section_costcentres.Qty2EstimatedTime
when 3 then
tbl_section_costcentres.Qty3EstimatedTime
end

as EstimatedTime

FROM tbl_Items,tbl_item_sections,tbl_section_costcentres
WHERE tbl_Items.ItemID = tbl_item_sections.ItemID and tbl_item_sections.ItemSectionID =tbl_section_costcentres.ItemSectionID
and tbl_Items.ItemID = @ItemID
ORDER BY SectionCostcentreID,[Order]

open CostCenterTime_cursor 
FETCH NEXT FROM CostCenterTime_cursor into @CostCenterID,@CostCenterDuration
WHILE (@@FETCH_STATUS = 0)
BEGIN


print convert(varchar(50),@CostCenterStartTime)


  FETCH NEXT FROM CostCenterTime_cursor into @CostCenterID,@CostCenterDuration    
   
  set @CostCenterStartTime = DATEADD(hour,isnull(@CostCenterDuration,0),@CostCenterStartTime)
  set @CostCenterStartTime = DATEADD(minute,1,@CostCenterStartTime)
  print isnull(@CostCenterDuration,0)
  print @CostCenterStartTime
  
END

close CostCenterTime_cursor

Select @CostCenterStartTime

DEALLOCATE CostCenterTime_cursor

RETURN