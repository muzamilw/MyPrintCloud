CREATE TABLE [dbo].[CmsOffer] (
    [OfferId]     INT           IDENTITY (1, 1) NOT NULL,
    [ItemId]      INT           NULL,
    [OfferType]   INT           NULL,
    [Description] VARCHAR (100) NULL,
    [ItemName]    VARCHAR (100) NULL,
    [SortOrder]   INT           NULL,
    [CompanyId]   BIGINT        NULL,
    CONSTRAINT [PK_tbl_cmsOffers] PRIMARY KEY CLUSTERED ([OfferId] ASC),
    CONSTRAINT [FK_CmsOffer_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

