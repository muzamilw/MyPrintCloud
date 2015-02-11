CREATE TABLE [dbo].[CategoryTerritory] (
    [CategoryTerritoryId] BIGINT IDENTITY (1, 1) NOT NULL,
    [CompanyId]           BIGINT NULL,
    [ProductCategoryId]   BIGINT NULL,
    [TerritoryId]         BIGINT NULL,
    [OrganisationId]      BIGINT NULL,
    CONSTRAINT [PK_tbl_CategoryTerritories] PRIMARY KEY CLUSTERED ([CategoryTerritoryId] ASC)
);

