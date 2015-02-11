CREATE PROCEDURE [dbo].[sp_Item_get_ItemForJobProgress]

	(
@EstimateID int,
@isForAllItems int
	)

AS
if @isForAllItems =0
	 begin 
			SELECT tbl_items.ItemID,tbl_items.ProductName as Title,tbl_items.EstimateDescription,
			tbl_items.JobCode,tbl_items.JobManagerID,tbl_items.JobCreationDateTime,tbl_items.JobStatusID,tbl_items.EstimateDescriptionTitle1,
			tbl_items.EstimateDescriptionTitle2,tbl_items.EstimateDescriptionTitle3,tbl_items.EstimateDescriptionTitle4,tbl_items.EstimateDescriptionTitle5,
			tbl_items.EstimateDescriptionTitle6,tbl_items.EstimateDescriptionTitle7,tbl_items.EstimateDescriptionTitle8,tbl_items.EstimateDescriptionTitle9,
			tbl_items.EstimateDescriptionTitle10,tbl_items.EstimateDescription1,tbl_items.EstimateDescription2,tbl_items.EstimateDescription3,
			tbl_items.EstimateDescription4,tbl_items.EstimateDescription5,tbl_items.EstimateDescription6,tbl_items.EstimateDescription7,
			tbl_items.EstimateDescription8, tbl_items.EstimateDescription9,tbl_items.EstimateDescription10,tbl_items.JobDescriptionTitle1,
			tbl_items.JobDescriptionTitle2,tbl_items.JobDescriptionTitle3,tbl_items.JobDescriptionTitle4,tbl_items.JobDescriptionTitle5,
			tbl_items.JobDescriptionTitle6,tbl_items.JobDescriptionTitle7,tbl_items.JobDescriptionTitle8,tbl_items.JobDescriptionTitle9,
			tbl_items.JobDescriptionTitle10,tbl_items.JobDescription1,tbl_items.JobDescription2,tbl_items.JobDescription3,tbl_items.JobDescription4,
			tbl_items.JobDescription5,tbl_items.JobDescription6,tbl_items.JobDescription7,tbl_items.JobDescription8,tbl_items.JobDescription9,tbl_items.JobDescription10,
			tbl_items.JobDescription,tbl_items.Status,tbl_items.IsGroupItem,tbl_items.JobProgressedBy,tbl_items.JobEstimatedCompletionDateTime,tbl_items.JobEstimatedStartDateTime 
			FROM tbl_items 
			WHERE (tbl_items.EstimateID = @EstimateID) --and tbl_items.status = 17  --and (tbl_items.itemtype <> 2 or tbl_items.itemtype is null)
			-- and (tbl_items.Status=2 or tbl_items.IsGroupItem <> 0)
       end  
  else
       begin

       		SELECT tbl_items.ItemID,tbl_items.ProductName as Title,tbl_items.EstimateDescription,
			tbl_items.JobCode,tbl_items.JobManagerID,tbl_items.JobCreationDateTime,tbl_items.JobStatusID,tbl_items.EstimateDescriptionTitle1,
			tbl_items.EstimateDescriptionTitle2,tbl_items.EstimateDescriptionTitle3,tbl_items.EstimateDescriptionTitle4,tbl_items.EstimateDescriptionTitle5,
			tbl_items.EstimateDescriptionTitle6,tbl_items.EstimateDescriptionTitle7,tbl_items.EstimateDescriptionTitle8,tbl_items.EstimateDescriptionTitle9,
			tbl_items.EstimateDescriptionTitle10,tbl_items.EstimateDescription1,tbl_items.EstimateDescription2,tbl_items.EstimateDescription3,
			tbl_items.EstimateDescription4,tbl_items.EstimateDescription5,tbl_items.EstimateDescription6,tbl_items.EstimateDescription7,
			tbl_items.EstimateDescription8, tbl_items.EstimateDescription9,tbl_items.EstimateDescription10,tbl_items.JobDescriptionTitle1,
			tbl_items.JobDescriptionTitle2,tbl_items.JobDescriptionTitle3,tbl_items.JobDescriptionTitle4,tbl_items.JobDescriptionTitle5,
			tbl_items.JobDescriptionTitle6,tbl_items.JobDescriptionTitle7,tbl_items.JobDescriptionTitle8,tbl_items.JobDescriptionTitle9,
			tbl_items.JobDescriptionTitle10,tbl_items.JobDescription1,tbl_items.JobDescription2,tbl_items.JobDescription3,tbl_items.JobDescription4,
			tbl_items.JobDescription5,tbl_items.JobDescription6,tbl_items.JobDescription7,tbl_items.JobDescription8,tbl_items.JobDescription9,tbl_items.JobDescription10,
			tbl_items.JobDescription,tbl_items.Status,tbl_items.IsGroupItem,tbl_items.JobProgressedBy,tbl_items.JobEstimatedCompletionDateTime,tbl_items.JobEstimatedStartDateTime 
			 FROM tbl_items 
			WHERE tbl_items.EstimateID = @EstimateID -- and tbl_items.status = 17 --and (tbl_items.itemtype <> 2 or tbl_items.itemtype is null)
			--
       
		end
  
  RETURN