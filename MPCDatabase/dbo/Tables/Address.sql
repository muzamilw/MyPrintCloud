CREATE TABLE [dbo].[Address] (
    [AddressId]                  BIGINT        IDENTITY (1, 1) NOT NULL,
    [CompanyId]                  BIGINT        NULL,
    [AddressName]                VARCHAR (100) NULL,
    [Address1]                   VARCHAR (255) NULL,
    [Address2]                   VARCHAR (255) NULL,
    [Address3]                   VARCHAR (255) NULL,
    [City]                       VARCHAR (100) NULL,
    [StateId]                    BIGINT        NULL,
    [CountryId]                  BIGINT        NULL,
    [PostCode]                   VARCHAR (30)  NULL,
    [Fax]                        VARCHAR (30)  NULL,
    [Email]                      VARCHAR (150) NULL,
    [URL]                        VARCHAR (100) NULL,
    [Tel1]                       VARCHAR (60)  NULL,
    [Tel2]                       VARCHAR (30)  NULL,
    [Extension1]                 VARCHAR (7)   NULL,
    [Extension2]                 VARCHAR (7)   NULL,
    [Reference]                  VARCHAR (50)  NULL,
    [FAO]                        VARCHAR (50)  NULL,
    [IsDefaultAddress]           BIT           NULL,
    [IsDefaultShippingAddress]   BIT           NULL,
    [isArchived]                 BIT           NULL,
    [TerritoryId]                BIGINT        NULL,
    [GeoLatitude]                VARCHAR (100) NULL,
    [GeoLongitude]               VARCHAR (100) NULL,
    [isPrivate]                  BIT           NULL,
    [ContactId]                  BIGINT        NULL,
    [isDefaultTerrorityBilling]  BIT           NULL,
    [isDefaultTerrorityShipping] BIT           NULL,
    [OrganisationId]             BIGINT        NULL,
    [DisplayOnContactUs]         BIT           NULL,
    CONSTRAINT [PK_tbl_customeraddresses] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([CountryID]),
    FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([StateId]),
    CONSTRAINT [FK_tbl_addresses_tbl_contactcompanies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_tbl_addresses_tbl_CustomerTerritory] FOREIGN KEY ([TerritoryId]) REFERENCES [dbo].[CompanyTerritory] ([TerritoryId])
);


GO
CREATE NONCLUSTERED INDEX [CustomerID]
    ON [dbo].[Address]([CompanyId] ASC);

