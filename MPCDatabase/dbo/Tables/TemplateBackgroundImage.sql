CREATE TABLE [dbo].[TemplateBackgroundImage] (
    [Id]                          BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductId]                   BIGINT         NULL,
    [ImageName]                   VARCHAR (300)  NULL,
    [Name]                        VARCHAR (300)  NULL,
    [flgPhotobook]                BIT            NULL,
    [flgCover]                    BIT            NULL,
    [BackgroundImageAbsolutePath] NVARCHAR (500) NULL,
    [BackgroundImageRelativePath] NVARCHAR (500) NULL,
    [ImageType]                   INT            NULL,
    [ImageWidth]                  INT            NULL,
    [ImageHeight]                 INT            NULL,
    [ImageTitle]                  NVARCHAR (MAX) NULL,
    [ImageDescription]            NVARCHAR (MAX) NULL,
    [ImageKeywords]               NVARCHAR (MAX) NULL,
    [UploadedFrom]                INT            NULL,
    [ContactCompanyId]            BIGINT         NULL,
    [ContactId]                   BIGINT         NULL,
    CONSTRAINT [PK_ProductBackgroundImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__TemplateB__Produ__593384D5] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Template] ([ProductId])
);

