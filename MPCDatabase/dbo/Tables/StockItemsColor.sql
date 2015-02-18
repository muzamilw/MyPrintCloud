CREATE TABLE [dbo].[StockItemsColor] (
    [StockColorId] INT           IDENTITY (1, 1) NOT NULL,
    [StockItemId]  BIGINT        NULL,
    [DisplayName]  VARCHAR (50)  NULL,
    [ColorHex]     VARCHAR (50)  NULL,
    [isRGB]        BIT           NULL,
    [ColorA]       VARCHAR (10)  NULL,
    [ColorB]       VARCHAR (10)  NULL,
    [ColorC]       VARCHAR (10)  NULL,
    [ColorD]       VARCHAR (10)  NULL,
    [Notes]        VARCHAR (250) NULL,
    [Ref]          VARCHAR (50)  NULL,
    CONSTRAINT [PK_tbl_stockitems_colors] PRIMARY KEY CLUSTERED ([StockColorId] ASC),
    CONSTRAINT [FK_tbl_stockitems_colors_tbl_stockitems] FOREIGN KEY ([StockItemId]) REFERENCES [dbo].[StockItem] ([StockItemId])
);

