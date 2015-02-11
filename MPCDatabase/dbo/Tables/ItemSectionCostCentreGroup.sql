CREATE TABLE [dbo].[ItemSectionCostCentreGroup] (
    [Id]                INT    IDENTITY (1, 1) NOT NULL,
    [ItemSectionId]     BIGINT NOT NULL,
    [CostCentreGroupId] INT    NULL,
    CONSTRAINT [PK_tbl_itemsection_costcentre_groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_item_sections_tbl_costcentre_groups] FOREIGN KEY ([ItemSectionId]) REFERENCES [dbo].[ItemSection] ([ItemSectionId]) ON DELETE CASCADE
);

