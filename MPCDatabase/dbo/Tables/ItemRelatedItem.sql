CREATE TABLE [dbo].[ItemRelatedItem] (
    [ItemId]        BIGINT NULL,
    [Id]            INT    IDENTITY (1, 1) NOT NULL,
    [RelatedItemId] BIGINT NULL,
    CONSTRAINT [PK_tbl_Product_RelatedProducts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_items_RelatedItems_tbl_items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]) ON DELETE CASCADE,
    CONSTRAINT [FK_tbl_items_RelatedItems_tbl_items1] FOREIGN KEY ([RelatedItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

