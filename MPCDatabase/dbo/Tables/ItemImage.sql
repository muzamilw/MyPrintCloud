CREATE TABLE [dbo].[ItemImage] (
    [ProductImageId] INT           IDENTITY (1, 1) NOT NULL,
    [ItemId]         BIGINT        NULL,
    [ImageTitle]     VARCHAR (100) NULL,
    [ImageURL]       VARCHAR (200) NULL,
    [ImageType]      VARCHAR (10)  NULL,
    [ImageName]      VARCHAR (100) NULL,
    [UploadDate]     DATETIME      NULL,
    CONSTRAINT [PK_tbl_itemImages] PRIMARY KEY CLUSTERED ([ProductImageId] ASC),
    CONSTRAINT [itemIDfk] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);

