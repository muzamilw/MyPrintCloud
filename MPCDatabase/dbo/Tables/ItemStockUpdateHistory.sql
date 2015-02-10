CREATE TABLE [dbo].[ItemStockUpdateHistory] (
    [StockHistoryId]   INT      IDENTITY (1, 1) NOT NULL,
    [ItemId]           INT      NULL,
    [LastAvailableQty] INT      NULL,
    [LastModifiedQty]  INT      NULL,
    [LastOrderedQty]   INT      NULL,
    [ModifyEvent]      INT      NULL,
    [LastModifiedBy]   INT      NULL,
    [LastModifiedDate] DATETIME NULL,
    [OrderID]          INT      NULL,
    CONSTRAINT [PK_tbl_itemStockUpdateHistory] PRIMARY KEY CLUSTERED ([StockHistoryId] ASC)
);

