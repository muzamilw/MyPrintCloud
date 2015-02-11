CREATE TABLE [dbo].[ListingLink] (
    [LinkType]  VARCHAR (255) NULL,
    [LinkURL]   VARCHAR (255) NULL,
    [LinkTitle] VARCHAR (255) NULL,
    [ListingId] BIGINT        NULL,
    [LinkId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_ListingLink] PRIMARY KEY CLUSTERED ([LinkId] ASC)
);

