CREATE TABLE [dbo].[ItemVDPPrice] (
    [ItemVDPPriceId] BIGINT     IDENTITY (1, 1) NOT NULL,
    [ClickRangeTo]   INT        NULL,
    [ClickRangeFrom] INT        NULL,
    [PricePerClick]  FLOAT (53) NULL,
    [SetupCharge]    FLOAT (53) NULL,
    [ItemId]         BIGINT     NULL,
    PRIMARY KEY CLUSTERED ([ItemVDPPriceId] ASC),
    CONSTRAINT [FK_ItemVDPPrice_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

