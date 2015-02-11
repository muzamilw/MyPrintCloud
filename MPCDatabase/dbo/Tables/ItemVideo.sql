CREATE TABLE [dbo].[ItemVideo] (
    [VideoId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [VideoLink] VARCHAR (255) NULL,
    [Caption]   VARCHAR (255) NULL,
    [ItemId]    BIGINT        NULL,
    CONSTRAINT [PK_ItemVideo] PRIMARY KEY CLUSTERED ([VideoId] ASC),
    CONSTRAINT [FK_ItemVideo_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

