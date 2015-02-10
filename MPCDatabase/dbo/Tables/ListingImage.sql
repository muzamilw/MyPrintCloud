CREATE TABLE [dbo].[ListingImage] (
    [ListingImageId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [ListingId]      BIGINT        NULL,
    [ImageURL]       VARCHAR (500) NULL,
    [ImageType]      VARCHAR (10)  NULL,
    [ImageOrder]     INT           NULL,
    [LastMode]       DATETIME      NULL,
    [ImageRef]       VARCHAR (50)  NULL,
    [PropertyRef]    VARCHAR (50)  NULL,
    [ClientImageId]  VARCHAR (100) NULL,
    CONSTRAINT [PK_ListingImage] PRIMARY KEY CLUSTERED ([ListingImageId] ASC)
);

