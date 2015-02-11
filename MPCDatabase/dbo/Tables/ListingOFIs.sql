CREATE TABLE [dbo].[ListingOFIs] (
    [ListingOFIId]  BIGINT       IDENTITY (1, 1) NOT NULL,
    [ListingId]     BIGINT       NULL,
    [StartTime]     DATETIME     NULL,
    [EndTime]       DATETIME     NULL,
    [OFIRef]        VARCHAR (50) NULL,
    [ThirdPartyRef] VARCHAR (50) NULL,
    [PropertyRef]   VARCHAR (50) NULL,
    CONSTRAINT [PK_ListingOFId] PRIMARY KEY CLUSTERED ([ListingOFIId] ASC)
);

