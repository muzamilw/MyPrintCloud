CREATE TABLE [dbo].[StockCostAndPrice] (
    [CostPriceId]           INT        IDENTITY (1, 1) NOT NULL,
    [ItemId]                BIGINT     CONSTRAINT [DF__tbl_stock__ItemI__25FB978D] DEFAULT ((0)) NOT NULL,
    [CostPrice]             FLOAT (53) CONSTRAINT [DF__tbl_stock__CostP__26EFBBC6] DEFAULT ((0)) NOT NULL,
    [PackCostPrice]         FLOAT (53) CONSTRAINT [DF__tbl_stock__PackC__27E3DFFF] DEFAULT ((0)) NOT NULL,
    [FromDate]              DATETIME   NULL,
    [ToDate]                DATETIME   NULL,
    [CostOrPriceIdentifier] SMALLINT   CONSTRAINT [DF__tbl_stock__CostO__28D80438] DEFAULT ((0)) NOT NULL,
    [ProcessingCharge]      FLOAT (53) CONSTRAINT [DF__tbl_stock__Proce__29CC2871] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tbl_stock_cost_and_price] PRIMARY KEY CLUSTERED ([CostPriceId] ASC),
    CONSTRAINT [FK_tbl_stock_cost_and_price_tbl_stockitems] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[StockItem] ([StockItemId])
);

