CREATE TABLE [dbo].[StockSubCategory] (
    [SubCategoryId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [Code]          VARCHAR (5)   NULL,
    [Name]          VARCHAR (50)  NULL,
    [Description]   VARCHAR (255) NULL,
    [Fixed]         VARCHAR (1)   NOT NULL,
    [CategoryId]    BIGINT        CONSTRAINT [DF__tbl_stock__Categ__54B68676] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tbl_stocksubcategories] PRIMARY KEY CLUSTERED ([SubCategoryId] ASC),
    CONSTRAINT [FK_StockCategory_StockSubCategory] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[StockCategory] ([CategoryId]) ON DELETE CASCADE
);

