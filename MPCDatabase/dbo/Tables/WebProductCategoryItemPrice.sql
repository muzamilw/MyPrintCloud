CREATE TABLE [dbo].[WebProductCategoryItemPrice] (
    [ProductPriceId] NUMERIC (18)    NOT NULL,
    [ProductId]      NUMERIC (18)    NOT NULL,
    [Quantity]       INT             NOT NULL,
    [Price]          NUMERIC (18, 2) NOT NULL
);

