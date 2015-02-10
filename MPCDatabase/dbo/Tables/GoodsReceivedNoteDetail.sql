CREATE TABLE [dbo].[GoodsReceivedNoteDetail] (
    [GoodsReceivedDetailId] INT           IDENTITY (1, 1) NOT NULL,
    [ItemId]                INT           NULL,
    [QtyReceived]           FLOAT (53)    NULL,
    [GoodsreceivedId]       INT           CONSTRAINT [DF__tbl_goods__Goods__5A846E65] DEFAULT (0) NULL,
    [Price]                 FLOAT (53)    CONSTRAINT [DF__tbl_goods__Price__5B78929E] DEFAULT (0) NULL,
    [PackQty]               INT           NULL,
    [TotalOrderedqty]       FLOAT (53)    NULL,
    [Details]               TEXT          NULL,
    [ItemCode]              VARCHAR (50)  NULL,
    [Name]                  VARCHAR (255) NULL,
    [TotalPrice]            FLOAT (53)    NULL,
    [TaxId]                 INT           NULL,
    [NetTax]                FLOAT (53)    NULL,
    [Discount]              FLOAT (53)    NULL,
    [FreeItems]             INT           CONSTRAINT [DF_tbl_goodsreceivednotedetail_FreeItems] DEFAULT (0) NULL,
    [DepartmentId]          INT           NULL,
    CONSTRAINT [PK_tbl_goodsreceivednotedetail] PRIMARY KEY CLUSTERED ([GoodsReceivedDetailId] ASC),
    CONSTRAINT [FK_GoodsreceivedID] FOREIGN KEY ([GoodsreceivedId]) REFERENCES [dbo].[GoodsReceivedNote] ([GoodsReceivedId])
);

