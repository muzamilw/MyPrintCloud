CREATE TABLE [dbo].[CompanyTerritory] (
    [TerritoryId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [TerritoryName] VARCHAR (200) NULL,
    [CompanyId]     BIGINT        NULL,
    [TerritoryCode] VARCHAR (200) NULL,
    [isDefault]     BIT           NULL,
    CONSTRAINT [PK_tbl_CustomerTerritory] PRIMARY KEY CLUSTERED ([TerritoryId] ASC),
    CONSTRAINT [FK_tbl_CustomerTerritory_tbl_contactcompanies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

