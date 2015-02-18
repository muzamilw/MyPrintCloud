CREATE TABLE [dbo].[GlobalLanguage] (
    [LanguageId]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [FriendlyName] VARCHAR (50) NULL,
    [uiCulture]    VARCHAR (50) NULL,
    [culture]      VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_language] PRIMARY KEY CLUSTERED ([LanguageId] ASC)
);

