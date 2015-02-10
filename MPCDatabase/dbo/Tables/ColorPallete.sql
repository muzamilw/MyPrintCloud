CREATE TABLE [dbo].[ColorPallete] (
    [PalleteId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [PalleteName] VARCHAR (200) NULL,
    [Color1]      VARCHAR (15)  NULL,
    [Color2]      VARCHAR (15)  NULL,
    [Color3]      VARCHAR (15)  NULL,
    [Color4]      VARCHAR (15)  NULL,
    [Color5]      VARCHAR (15)  NULL,
    [Color6]      VARCHAR (15)  NULL,
    [Color7]      VARCHAR (15)  NULL,
    [SkinId]      BIGINT        NULL,
    [isDefault]   BIT           NULL,
    [CompanyId]   BIGINT        NULL,
    CONSTRAINT [PK_tbl_cmsColorPalletes] PRIMARY KEY CLUSTERED ([PalleteId] ASC),
    CONSTRAINT [FK_ColorPallete_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_tbl_cmsSkins_tbl_cmsColorPalletes] FOREIGN KEY ([SkinId]) REFERENCES [dbo].[tbl_cmsSkins] ([SkinId])
);

