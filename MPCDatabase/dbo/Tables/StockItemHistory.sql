CREATE TABLE [dbo].[StockItemHistory] (
    [ItemHId]      INT        IDENTITY (1, 1) NOT NULL,
    [ChangeDate]   DATETIME   NULL,
    [UserId]       INT        NULL,
    [OldPackPrice] FLOAT (53) NULL,
    [NewPackPrice] FLOAT (53) NULL,
    [GRNId]        INT        CONSTRAINT [DF__tbl_stock__GRNID__381A47C8] DEFAULT (0) NOT NULL,
    [ItemId]       INT        NULL,
    CONSTRAINT [PK_tbl_stockitem_history] PRIMARY KEY CLUSTERED ([ItemHId] ASC)
);

