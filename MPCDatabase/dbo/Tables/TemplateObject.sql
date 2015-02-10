﻿CREATE TABLE [dbo].[TemplateObject] (
    [ObjectId]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [ObjectType]             INT            NULL,
    [Name]                   VARCHAR (200)  NOT NULL,
    [IsEditable]             BIT            NULL,
    [IsHidden]               BIT            NULL,
    [IsMandatory]            BIT            NULL,
    [PositionX]              FLOAT (53)     NULL,
    [PositionY]              FLOAT (53)     NULL,
    [MaxHeight]              FLOAT (53)     NULL,
    [MaxWidth]               FLOAT (53)     NULL,
    [MaxCharacters]          FLOAT (53)     NULL,
    [RotationAngle]          FLOAT (53)     NULL,
    [IsFontCustom]           BIT            NULL,
    [IsFontNamePrivate]      BIT            NULL,
    [FontName]               VARCHAR (200)  NULL,
    [FontSize]               FLOAT (53)     NULL,
    [IsBold]                 BIT            NULL,
    [IsItalic]               BIT            NULL,
    [Allignment]             INT            NULL,
    [VAllignment]            INT            NULL,
    [Indent]                 FLOAT (53)     NULL,
    [IsUnderlinedText]       BIT            NULL,
    [ColorType]              INT            NULL,
    [ColorName]              VARCHAR (50)   NULL,
    [ColorC]                 INT            NULL,
    [ColorM]                 INT            NULL,
    [ColorY]                 INT            NULL,
    [ColorK]                 INT            NULL,
    [Tint]                   INT            NULL,
    [IsSpotColor]            BIT            NULL,
    [SpotColorName]          VARCHAR (150)  NULL,
    [ContentString]          NVARCHAR (MAX) NULL,
    [ContentCaseType]        INT            NULL,
    [ProductId]              BIGINT         NULL,
    [DisplayOrderPdf]        INT            NULL,
    [DisplayOrderTxtControl] INT            NULL,
    [RColor]                 INT            NULL,
    [GColor]                 INT            NULL,
    [BColor]                 INT            NULL,
    [LineSpacing]            FLOAT (53)     NULL,
    [ProductPageId]          BIGINT         NULL,
    [ParentId]               BIGINT         NULL,
    [CircleRadiusX]          FLOAT (53)     NULL,
    [Opacity]                FLOAT (53)     NULL,
    [ExField1]               NVARCHAR (50)  NULL,
    [ExField2]               NVARCHAR (50)  NULL,
    [IsPositionLocked]       BIT            NULL,
    [ColorHex]               NVARCHAR (9)   NULL,
    [CircleRadiusY]          FLOAT (53)     NULL,
    [IsTextEditable]         BIT            NULL,
    [QuickTextOrder]         INT            NULL,
    [IsQuickText]            BIT            NULL,
    [CharSpacing]            FLOAT (53)     NULL,
    [watermarkText]          NVARCHAR (255) NULL,
    [textStyles]             NVARCHAR (MAX) NULL,
    [AutoShrinkText]         BIT            NULL,
    [IsOverlayObject]        BIT            NULL,
    [ClippedInfo]            XML            NULL,
    [textCase]               INT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Objects] PRIMARY KEY CLUSTERED ([ObjectId] ASC),
    CONSTRAINT [FK__TemplateO__Produ__574B3C63] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Template] ([ProductId])
);

