CREATE TABLE [dbo].[TemplateFont] (
    [ProductFontId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductId]       BIGINT         NULL,
    [FontName]        NVARCHAR (50)  NULL,
    [FontDisplayName] NVARCHAR (50)  NULL,
    [FontFile]        NVARCHAR (200) NULL,
    [DisplayIndex]    INT            NULL,
    [IsPrivateFont]   BIT            NOT NULL,
    [IsEnable]        BIT            NULL,
    [FontBytes]       IMAGE          NULL,
    [FontPath]        NVARCHAR (MAX) NULL,
    [CustomerId]      BIGINT         NULL,
    CONSTRAINT [PK_ProductFonts] PRIMARY KEY CLUSTERED ([ProductFontId] ASC),
    CONSTRAINT [FK__TemplateF__Produ__5A27A90E] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Template] ([ProductId])
);

