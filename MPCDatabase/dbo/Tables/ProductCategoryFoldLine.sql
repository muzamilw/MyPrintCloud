CREATE TABLE [dbo].[ProductCategoryFoldLine] (
    [FoldLineId]               INT        NOT NULL,
    [ProductCategoryId]        BIGINT     NULL,
    [FoldLineOrientation]      BIT        NULL,
    [FoldLineOffsetFromOrigin] FLOAT (53) NULL,
    CONSTRAINT [PK_tbl_ProductCategoryFoldLines] PRIMARY KEY CLUSTERED ([FoldLineId] ASC),
    CONSTRAINT [FK_tbl_ProductCategoryFoldLines_tbl_ProductCategory] FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
);

