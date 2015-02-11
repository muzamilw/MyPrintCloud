CREATE TABLE [dbo].[ItemStockOption] (
    [ItemStockOptionId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [ItemId]            BIGINT        NULL,
    [OptionSequence]    INT           NULL,
    [StockId]           BIGINT        NULL,
    [StockLabel]        VARCHAR (255) NULL,
    [CompanyId]         BIGINT        NULL,
    [ImageURL]          VARCHAR (255) NULL,
    CONSTRAINT [PK_tbl_ItemStockOptions] PRIMARY KEY CLUSTERED ([ItemStockOptionId] ASC),
    CONSTRAINT [FK_StockItem_ItemStockOption] FOREIGN KEY ([StockId]) REFERENCES [dbo].[StockItem] ([StockItemId]),
    CONSTRAINT [itemmidfk] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

