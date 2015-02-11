CREATE TABLE [dbo].[TemplatePage] (
    [ProductPageId]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductId]          BIGINT         NULL,
    [PageNo]             INT            NULL,
    [PageType]           INT            NULL,
    [Orientation]        INT            NULL,
    [BackGroundType]     INT            NULL,
    [BackgroundFileName] NVARCHAR (MAX) NULL,
    [ColorC]             INT            NULL,
    [ColorM]             INT            NULL,
    [ColorY]             INT            NULL,
    [PageName]           NVARCHAR (100) NULL,
    [ColorK]             INT            NULL,
    [IsPrintable]        BIT            NULL,
    [hasOverlayObjects]  BIT            NULL,
    [Width]              FLOAT (53)     NULL,
    [Height]             FLOAT (53)     NULL,
    CONSTRAINT [PK_TemplatePages] PRIMARY KEY CLUSTERED ([ProductPageId] ASC),
    CONSTRAINT [FK__TemplateP__Produ__5C0FF180] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Template] ([ProductId])
);

