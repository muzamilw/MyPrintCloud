CREATE TABLE [dbo].[Country] (
    [CountryID]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [CountryName] VARCHAR (50) NOT NULL,
    [CountryCode] VARCHAR (10) NULL,
    CONSTRAINT [PK_tbl_country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

