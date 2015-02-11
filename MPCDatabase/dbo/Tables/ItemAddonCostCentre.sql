CREATE TABLE [dbo].[ItemAddonCostCentre] (
    [ProductAddOnId]     INT        IDENTITY (1, 1) NOT NULL,
    [ItemStockOptionId]  BIGINT     NULL,
    [CostCentreId]       BIGINT     NULL,
    [DiscountPercentage] FLOAT (53) CONSTRAINT [DF__tbl_Produ__isDis__124B3B49] DEFAULT ((0)) NULL,
    [IsDiscounted]       BIT        CONSTRAINT [DF_tbl_Items_AddonCostCentres_IsDiscounted] DEFAULT ((0)) NULL,
    [Sequence]           INT        NULL,
    [IsMandatory]        BIT        NULL,
    CONSTRAINT [PK_tbl_Product_Quantity_AddOns] PRIMARY KEY CLUSTERED ([ProductAddOnId] ASC),
    CONSTRAINT [FK_ItemStockOption_ItemAddonCostCentre] FOREIGN KEY ([ItemStockOptionId]) REFERENCES [dbo].[ItemStockOption] ([ItemStockOptionId]),
    CONSTRAINT [FK_tbl_Items_AddonCostCentres_tbl_costcentres] FOREIGN KEY ([CostCentreId]) REFERENCES [dbo].[CostCentre] ([CostCentreId])
);

