CREATE TABLE [dbo].[CustomCopy] (
    [SignboardHeadline]    NVARCHAR (50) NULL,
    [SignboardDescription] NCHAR (10)    NULL,
    [BrochureHeadline]     NCHAR (10)    NULL,
    [BrochureDescription]  NCHAR (10)    NULL,
    [BrochureFeature1]     NCHAR (10)    NULL,
    [BrochureFeature2]     NCHAR (10)    NULL,
    [BrochureFeature3]     NCHAR (10)    NULL,
    [BrochureFeature4]     NCHAR (10)    NULL,
    [BrochureLifeStyle1]   NCHAR (10)    NULL,
    [BrochureLifeStyle2]   NCHAR (10)    NULL,
    [BrochureLifeStyle3]   NCHAR (10)    NULL,
    [ListingId]            BIGINT        NULL,
    [CustomCopyId]         BIGINT        IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_CustomCopy] PRIMARY KEY CLUSTERED ([CustomCopyId] ASC)
);

