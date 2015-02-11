CREATE TABLE [dbo].[TemplateColorStyle] (
    [PelleteId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [ProductId]     BIGINT        NULL,
    [Name]          VARCHAR (255) NULL,
    [ColorC]        INT           NULL,
    [ColorM]        INT           NULL,
    [ColorY]        INT           NULL,
    [ColorK]        INT           NULL,
    [SpotColor]     VARCHAR (50)  NULL,
    [IsSpotColor]   BIT           NULL,
    [Field1]        INT           NULL,
    [ColorHex]      NVARCHAR (30) NULL,
    [IsColorActive] BIT           NULL,
    [CustomerId]    BIGINT        NULL,
    CONSTRAINT [PK_ColorStyles] PRIMARY KEY CLUSTERED ([PelleteId] ASC),
    CONSTRAINT [FK__TemplateC__Produ__5B1BCD47] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Template] ([ProductId])
);

