CREATE TABLE [dbo].[CmsTags] (
    [TagId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [TagName]     VARCHAR (255) NULL,
    [TagSlug]     VARCHAR (255) NULL,
    [Description] VARCHAR (255) NULL,
    [isDisplay]   BIT           CONSTRAINT [DF__webcmsTag__idDis__797F8D7F] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_webcmsTags] PRIMARY KEY CLUSTERED ([TagId] ASC)
);

