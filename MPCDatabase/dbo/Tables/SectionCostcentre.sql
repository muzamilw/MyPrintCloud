﻿CREATE TABLE [dbo].[SectionCostcentre] (
    [SectionCostcentreId]             INT            IDENTITY (1, 1) NOT NULL,
    [ItemSectionId]                   BIGINT         CONSTRAINT [DF__tbl_secti__Secti__605D434C] DEFAULT ((0)) NULL,
    [CostCentreId]                    BIGINT         CONSTRAINT [DF__tbl_secti__CostC__61516785] DEFAULT ((0)) NULL,
    [CostCentreType]                  INT            CONSTRAINT [DF__tbl_secti__CostC__62458BBE] DEFAULT ((0)) NULL,
    [SystemCostCentreType]            INT            NULL,
    [Order]                           SMALLINT       CONSTRAINT [DF__tbl_secti__Order__6339AFF7] DEFAULT ((0)) NOT NULL,
    [IsDirectCost]                    SMALLINT       CONSTRAINT [DF__tbl_secti__IsDir__642DD430] DEFAULT ((0)) NULL,
    [IsOptionalExtra]                 SMALLINT       CONSTRAINT [DF__tbl_secti__IsOpt__6521F869] DEFAULT ((0)) NOT NULL,
    [IsPurchaseOrderRaised]           SMALLINT       CONSTRAINT [DF__tbl_secti__IsPur__66161CA2] DEFAULT ((0)) NULL,
    [Status]                          SMALLINT       NULL,
    [ActivityUser]                    INT            NULL,
    [IsPrintable]                     SMALLINT       CONSTRAINT [DF__tbl_secti__IsPri__670A40DB] DEFAULT ((1)) NULL,
    [EstimatedStartTime]              DATETIME       NULL,
    [EstimatedDuration]               INT            NULL,
    [EstimatedEndTime]                DATETIME       NULL,
    [ActualDuration]                  BIGINT         NULL,
    [ActualStartDateTime]             DATETIME       NULL,
    [ActualEndTime]                   DATETIME       NULL,
    [Qty1Charge]                      FLOAT (53)     NULL,
    [Qty2Charge]                      FLOAT (53)     NULL,
    [Qty3Charge]                      FLOAT (53)     NULL,
    [Qty4Charge]                      FLOAT (53)     NULL,
    [Qty5Charge]                      FLOAT (53)     NULL,
    [Qty1MarkUpID]                    INT            NULL,
    [Qty2MarkUpID]                    INT            NULL,
    [Qty3MarkUpID]                    INT            NULL,
    [Qty4MarkUpID]                    INT            NULL,
    [Qty5MarkUpID]                    INT            NULL,
    [Qty1MarkUpValue]                 FLOAT (53)     NULL,
    [Qty2MarkUpValue]                 FLOAT (53)     NULL,
    [Qty3MarkUpValue]                 FLOAT (53)     NULL,
    [Qty4MarkUpValue]                 FLOAT (53)     NULL,
    [Qty5MarkUpValue]                 FLOAT (53)     NULL,
    [Qty1NetTotal]                    FLOAT (53)     NULL,
    [Qty2NetTotal]                    FLOAT (53)     NULL,
    [Qty3NetTotal]                    FLOAT (53)     NULL,
    [Qty4NetTotal]                    FLOAT (53)     NULL,
    [Qty5NetTotal]                    FLOAT (53)     NULL,
    [Qty1EstimatedPlantCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1E__67FE6514] DEFAULT ((0)) NULL,
    [Qty1EstimatedLabourCost]         FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1E__68F2894D] DEFAULT ((0)) NULL,
    [Qty1EstimatedStockCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1E__69E6AD86] DEFAULT ((0)) NULL,
    [Qty1EstimatedTime]               FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1E__6ADAD1BF] DEFAULT ((0)) NOT NULL,
    [Qty1QuotedPlantCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1Q__6BCEF5F8] DEFAULT ((0)) NULL,
    [Qty1QuotedLabourCharge]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1Q__6CC31A31] DEFAULT ((0)) NULL,
    [Qty1QuotedStockCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty1Q__6DB73E6A] DEFAULT ((0)) NULL,
    [Qty2EstimatedPlantCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2E__6EAB62A3] DEFAULT ((0)) NULL,
    [Qty2EstimatedLabourCost]         FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2E__6F9F86DC] DEFAULT ((0)) NULL,
    [Qty2EstimatedStockCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2E__7093AB15] DEFAULT ((0)) NULL,
    [Qty2EstimatedTime]               FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2E__7187CF4E] DEFAULT ((0)) NOT NULL,
    [Qty2QuotedPlantCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2Q__727BF387] DEFAULT ((0)) NULL,
    [Qty2QuotedLabourCharge]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2Q__737017C0] DEFAULT ((0)) NULL,
    [Qty2QuotedStockCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty2Q__74643BF9] DEFAULT ((0)) NULL,
    [Qty3EstimatedPlantCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3E__75586032] DEFAULT ((0)) NULL,
    [Qty3EstimatedLabourCost]         FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3E__764C846B] DEFAULT ((0)) NULL,
    [Qty3EstimatedStockCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3E__7740A8A4] DEFAULT ((0)) NULL,
    [Qty3EstimatedTime]               FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3E__7834CCDD] DEFAULT ((0)) NOT NULL,
    [Qty3QuotedPlantCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3Q__7928F116] DEFAULT ((0)) NULL,
    [Qty3QuotedLabourCharge]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3Q__7A1D154F] DEFAULT ((0)) NULL,
    [Qty3QuotedStockCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty3Q__7B113988] DEFAULT ((0)) NULL,
    [Qty4EstimatedPlantCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4E__7C055DC1] DEFAULT ((0)) NULL,
    [Qty4EstimatedLabourCost]         FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4E__7CF981FA] DEFAULT ((0)) NULL,
    [Qty4EstimatedStockCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4E__7DEDA633] DEFAULT ((0)) NULL,
    [Qty4EstimatedTime]               FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4E__7EE1CA6C] DEFAULT ((0)) NOT NULL,
    [Qty4QuotedPlantCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4Q__7FD5EEA5] DEFAULT ((0)) NULL,
    [Qty4QuotedLabourCharge]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4Q__00CA12DE] DEFAULT ((0)) NULL,
    [Qty4QuotedStockCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty4Q__01BE3717] DEFAULT ((0)) NULL,
    [Qty5EstimatedPlantCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5E__02B25B50] DEFAULT ((0)) NULL,
    [Qty5EstimatedLabourCost]         FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5E__03A67F89] DEFAULT ((0)) NULL,
    [Qty5EstimatedStockCost]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5E__049AA3C2] DEFAULT ((0)) NULL,
    [Qty5EstimatedTime]               FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5E__058EC7FB] DEFAULT ((0)) NOT NULL,
    [Qty5QuotedPlantCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5Q__0682EC34] DEFAULT ((0)) NULL,
    [Qty5QuotedLabourCharge]          FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5Q__0777106D] DEFAULT ((0)) NULL,
    [Qty5QuotedStockCharge]           FLOAT (53)     CONSTRAINT [DF__tbl_secti__Qty5Q__086B34A6] DEFAULT ((0)) NULL,
    [ActualPlantCost]                 FLOAT (53)     CONSTRAINT [DF__tbl_secti__Actua__095F58DF] DEFAULT ((0)) NULL,
    [ActualLabourCost]                FLOAT (53)     CONSTRAINT [DF__tbl_secti__Actua__0A537D18] DEFAULT ((0)) NULL,
    [ActualStockCost]                 FLOAT (53)     CONSTRAINT [DF__tbl_secti__Actua__0B47A151] DEFAULT ((0)) NULL,
    [Qty1WorkInstructions]            TEXT           NULL,
    [Qty2WorkInstructions]            TEXT           NULL,
    [Qty3WorkInstructions]            TEXT           NULL,
    [Qty4WorkInstructions]            TEXT           NULL,
    [Qty5WorkInstructions]            TEXT           NULL,
    [IsCostCentreUsedinPurchaseOrder] SMALLINT       NULL,
    [IsMinimumCost]                   SMALLINT       CONSTRAINT [DF__tbl_secti__IsMin__0C3BC58A] DEFAULT ((0)) NOT NULL,
    [SetupTime]                       FLOAT (53)     CONSTRAINT [DF__tbl_secti__Setup__0D2FE9C3] DEFAULT ((0)) NOT NULL,
    [IsScheduled]                     SMALLINT       CONSTRAINT [DF__tbl_secti__IsSch__0E240DFC] DEFAULT ((0)) NOT NULL,
    [IsScheduleable]                  SMALLINT       CONSTRAINT [DF__tbl_secti__IsSch__0F183235] DEFAULT ((1)) NOT NULL,
    [Locked]                          BIT            NOT NULL,
    [CostingActualCost]               FLOAT (53)     NULL,
    [CostingActualTime]               FLOAT (53)     NULL,
    [CostingActualQty]                FLOAT (53)     NULL,
    [Name]                            NVARCHAR (100) NULL,
    [Qty1]                            INT            NULL,
    [Qty2]                            INT            NULL,
    [Qty3]                            INT            NULL,
    [SetupCost]                       FLOAT (53)     NULL,
    [PricePerUnitQty]                 FLOAT (53)     NULL,
    CONSTRAINT [PK_tbl_section_costcentres] PRIMARY KEY CLUSTERED ([SectionCostcentreId] ASC),
    CONSTRAINT [FK_tbl_section_costcentres_tbl_costcentres] FOREIGN KEY ([CostCentreId]) REFERENCES [dbo].[CostCentre] ([CostCentreId]),
    CONSTRAINT [FK_tbl_section_costcentres_tbl_item_sections] FOREIGN KEY ([ItemSectionId]) REFERENCES [dbo].[ItemSection] ([ItemSectionId]) ON DELETE CASCADE
);

