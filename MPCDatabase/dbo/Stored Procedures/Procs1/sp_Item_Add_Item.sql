﻿

CREATE PROCEDURE [dbo].[sp_Item_Add_Item]
(
	@jobSelectedQty int	,
	@FinishedGoodID int,
	@Tax2 int,
	@Tax3 int,
	@Qty1Tax2Value float,
	@Qty1Tax3Value float,
	@Qty2Tax2Value float,
	@Qty2Tax3Value float,
	@Qty3Tax2Value float,
	@Qty3Tax3Value float,
	@CanCopyToEstimate smallint,
	@ItemLastUpdateDateTime datetime,
	@ItemCode varchar (50),
	@IsItemLibraray smallint ,
	@ItemLibrarayGroupID int,
	@IsParagraphDescription smallint,
	@EstimateID int,
	@Title varchar (50),
	@Tax1 int,
	@CreatedBy int,
	@Status int,
	@ItemCreationDateTime datetime,
	@IsMultipleQty smallint,
	@RunOnQty int,
	@RunonBaseCharge float,
	@RunOnMarkUpID int,
	@RunOnMarkUpValue float,
	@RunOnNetTotal float,
	@Qty1 int,
	@Qty2 int,
	@Qty3 int,
	@Qty1BaseCharge1 float,
	@Qty2BaseCharge2 float,
	@Qty3BaseCharge3 float,
	@Qty1MarkUpID1 int,
	@Qty2MarkUpID2 int,
	@Qty3MarkUpID3 int,
	@Qty1MarkUp1Value float,
	@Qty2MarkUp2Value float,
	@Qty3MarkUp3Value float,
	@Qty1NetTotal float,
	@Qty2NetTotal float,
	@Qty3NetTotal float,
	
	@Qty1Tax1Value float,
	@Qty1GrossTotal float,
	@Qty2Tax1Value float,
	@Qty2grossTotal float,
	@Qty3Tax1Value float,
	@Qty3GrossTotal float,
	@IsDescriptionLocked smallint,
	@EstimateDescriptionTitle1 text,
	@EstimateDescriptionTitle2 text,
	@EstimateDescriptionTitle3 text,
	@EstimateDescriptionTitle4 text,
	@EstimateDescriptionTitle5 text,
	@EstimateDescriptionTitle6 text,
	@EstimateDescriptionTitle7 text,
	@EstimateDescriptionTitle8 text,
	@EstimateDescriptionTitle9 text,
	@EstimateDescriptionTitle10 text,
	@EstimateDescription1 text,
	@EstimateDescription2 text,
	@EstimateDescription3 text,
	@EstimateDescription4 text,
	@EstimateDescription5 text,
	@EstimateDescription6 text,
	@EstimateDescription7 text,
	@EstimateDescription8 text,
	@EstimateDescription9 text,
	@EstimateDescription10 text,
	@EstimateDescription text,
	@InvoiceDescription text,
	@qty1Title varchar (50),
	@qty2Title varchar (50),
	@qty3Title varchar (50),
	@RunonTitle varchar (50),
	@AdditionalInformation text,
	@qty2Description text,
	@qty3Description text,
	@RunonDescription text,
	@Qty1MarkUpPercentageValue float,
	@Qty2MarkUpPercentageValue float,
	@Qty3MarkUpPercentageValue float,
	@RunonPercentageValue float,
	@RunonCostCentreProfit float,
	@Qty1CostCentreProfit float,
	@Qty2CostCentreProfit float,
	@Qty3CostCentreProfit float,
	@ItemType int,
	@IsIncludedInPipeLine smallint,
	@IsRunOnQty smallint,
	@IsFinishedGoodPrivate smallint,
	@IsGroupItem smallint,
	@CostCenterDescriptions image = null,
	@FlagID int,
	@NominalCode int,
	@DepartmentID int,
	@ItemNotes text,
	@UpdatedBy	varchar(50)	,
	@LastUpdate	datetime	

)
AS
	insert into tbl_items (jobSelectedQty,FinishedGoodID,Tax2,Tax3,Qty1Tax2Value,Qty1Tax3Value,Qty2Tax2Value,Qty2Tax3Value,Qty3Tax2Value,Qty3Tax3Value,CanCopyToEstimate,ItemLastUpdateDateTime,ItemCode,IsItemLibraray,ItemLibrarayGroupID,IsParagraphDescription,EstimateID,Title,Tax1,CreatedBy,Status,ItemCreationDateTime,IsMultipleQty,RunOnQty,RunonBaseCharge,RunOnMarkUpID,RunOnMarkUpValue,RunOnNetTotal,Qty1,Qty2,Qty3,Qty1BaseCharge1,Qty2BaseCharge2,Qty3BaseCharge3,Qty1MarkUpID1,Qty2MarkUpID2,Qty3MarkUpID3,Qty1MarkUp1Value,Qty2MarkUp2Value,Qty3MarkUp3Value,Qty1NetTotal,Qty2NetTotal,Qty3NetTotal,Qty1Tax1Value,Qty1GrossTotal,Qty2Tax1Value,Qty2grossTotal,Qty3Tax1Value,Qty3GrossTotal,IsDescriptionLocked,EstimateDescriptionTitle1,EstimateDescriptionTitle2,EstimateDescriptionTitle3,EstimateDescriptionTitle4,EstimateDescriptionTitle5,EstimateDescriptionTitle6,EstimateDescriptionTitle7,EstimateDescriptionTitle8,EstimateDescriptionTitle9,EstimateDescriptionTitle10,EstimateDescription1,EstimateDescription2,EstimateDescription3,EstimateDescription4,EstimateDescription5,EstimateDescription6,EstimateDescription7,EstimateDescription8,EstimateDescription9,EstimateDescription10,EstimateDescription,qty1Title,qty2Title,qty3Title,RunonTitle,AdditionalInformation,qty2Description,qty3Description,RunonDescription,Qty1MarkUpPercentageValue,Qty2MarkUpPercentageValue,Qty3MarkUpPercentageValue,RunonPercentageValue,RunonCostCentreProfit,Qty1CostCentreProfit,Qty2CostCentreProfit,Qty3CostCentreProfit,ItemType,IsIncludedInPipeLine , IsRunOnQty,IsFinishedGoodPrivate,IsGroupItem, CostCenterDescriptions,FlagID,NominalCodeID,InvoiceDescription,DepartmentID,ItemNotes,UpdatedBy,LastUpdate,ProductName	) VALUES 
                                            (@jobSelectedQty,@FinishedGoodID,@Tax2,@Tax3,@Qty1Tax2Value,@Qty1Tax3Value,@Qty2Tax2Value,@Qty2Tax3Value,@Qty3Tax2Value,@Qty3Tax3Value,@CanCopyToEstimate,@ItemLastUpdateDateTime,@ItemCode,@IsItemLibraray,@ItemLibrarayGroupID,@IsParagraphDescription,@EstimateID,@Title,@Tax1,@CreatedBy,@Status,@ItemCreationDateTime,@IsMultipleQty,@RunOnQty,@RunonBaseCharge,@RunOnMarkUpID,@RunOnMarkUpValue,@RunOnNetTotal,@Qty1,@Qty2,@Qty3,@Qty1BaseCharge1,@Qty2BaseCharge2,@Qty3BaseCharge3,@Qty1MarkUpID1,@Qty2MarkUpID2,@Qty3MarkUpID3,@Qty1MarkUp1Value,@Qty2MarkUp2Value,@Qty3MarkUp3Value,@Qty1NetTotal,@Qty2NetTotal,@Qty3NetTotal,@Qty1Tax1Value,@Qty1GrossTotal,@Qty2Tax1Value,@Qty2grossTotal,@Qty3Tax1Value,@Qty3GrossTotal,@IsDescriptionLocked,@EstimateDescriptionTitle1,@EstimateDescriptionTitle2,@EstimateDescriptionTitle3,@EstimateDescriptionTitle4,@EstimateDescriptionTitle5,@EstimateDescriptionTitle6,@EstimateDescriptionTitle7,@EstimateDescriptionTitle8,@EstimateDescriptionTitle9,@EstimateDescriptionTitle10,@EstimateDescription1,@EstimateDescription2,@EstimateDescription3,@EstimateDescription4,@EstimateDescription5,@EstimateDescription6,@EstimateDescription7,@EstimateDescription8,@EstimateDescription9,@EstimateDescription10,@EstimateDescription,@qty1Title,@qty2Title,@qty3Title,@RunonTitle,@AdditionalInformation,@qty2Description,@qty3Description,@RunonDescription,@Qty1MarkUpPercentageValue,@Qty2MarkUpPercentageValue,@Qty3MarkUpPercentageValue,@RunonPercentageValue,@RunonCostCentreProfit,@Qty1CostCentreProfit,@Qty2CostCentreProfit,@Qty3CostCentreProfit,@ItemType,@IsIncludedInPipeLine,@IsRunOnQty,@IsFinishedGoodPrivate,@IsGroupItem,@CostCenterDescriptions,@FlagID,@NominalCode,@InvoiceDescription,@DepartmentID,@ItemNotes,@UpdatedBy,@LastUpdate,@Title)
SELECT @@IDENTITY AS ItemID 
	RETURN