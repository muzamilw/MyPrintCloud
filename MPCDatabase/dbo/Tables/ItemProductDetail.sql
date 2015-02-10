CREATE TABLE [dbo].[ItemProductDetail] (
    [ItemDetailId]                 INT            IDENTITY (1, 1) NOT NULL,
    [ItemId]                       BIGINT         NULL,
    [isInternalActivity]           BIT            NULL,
    [isAutoCreateSupplierPO]       BIT            NULL,
    [isQtyLimit]                   BIT            NULL,
    [QtyLimit]                     INT            NULL,
    [DeliveryTimeSupplier1]        INT            NULL,
    [DeliveryTimeSupplier2]        INT            NULL,
    [isPrintItem]                  BIT            NULL,
    [isAllowMarketBriefAttachment] BIT            NULL,
    [MarketBriefSuccessMessage]    VARCHAR (5000) NULL,
    CONSTRAINT [PK_tbl_items_ProductDetails] PRIMARY KEY CLUSTERED ([ItemDetailId] ASC),
    CONSTRAINT [FK_ItemProductDetail_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

