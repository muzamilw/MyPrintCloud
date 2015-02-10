CREATE TABLE [dbo].[ProductCategoryItem] (
    [ProductCategoryItemId] BIGINT IDENTITY (1, 1) NOT NULL,
    [CategoryId]            BIGINT NULL,
    [ItemId]                BIGINT NULL,
    PRIMARY KEY CLUSTERED ([ProductCategoryItemId] ASC),
    CONSTRAINT [FK_ProductCategoryItem_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]),
    CONSTRAINT [FK_ProductCategoryItem_ProductCategory] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
);

