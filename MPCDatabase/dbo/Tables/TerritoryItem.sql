CREATE TABLE [dbo].[TerritoryItem] (
    [TerritoryItemId] INT    IDENTITY (1, 1) NOT NULL,
    [TerritoryId]     BIGINT NULL,
    [ItemId]          BIGINT NULL,
    CONSTRAINT [PK_tbl_TerritoryProducts] PRIMARY KEY CLUSTERED ([TerritoryItemId] ASC),
    CONSTRAINT [FK_tbl_TerritoryItems_tbl_ContactCompanyTerritories] FOREIGN KEY ([TerritoryId]) REFERENCES [dbo].[CompanyTerritory] ([TerritoryId]),
    CONSTRAINT [FK_tbl_TerritoryItems_tbl_items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

