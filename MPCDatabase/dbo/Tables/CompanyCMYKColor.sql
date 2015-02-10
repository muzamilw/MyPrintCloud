CREATE TABLE [dbo].[CompanyCMYKColor] (
    [ColorId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [CompanyId] BIGINT        NULL,
    [ColorName] VARCHAR (200) NULL,
    [ColorC]    VARCHAR (50)  NULL,
    [ColorM]    VARCHAR (50)  NULL,
    [ColorY]    VARCHAR (50)  NULL,
    [ColorK]    VARCHAR (50)  NULL,
    CONSTRAINT [PK_CompanyCMYKColor] PRIMARY KEY CLUSTERED ([ColorId] ASC),
    CONSTRAINT [FK_CompanyCMYKColor_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

