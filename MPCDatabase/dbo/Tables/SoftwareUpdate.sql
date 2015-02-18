CREATE TABLE [dbo].[SoftwareUpdate] (
    [PatchVersion]    VARCHAR (50)   NOT NULL,
    [PatchTitle]      VARCHAR (100)  NULL,
    [InstalledOnDate] DATETIME       NULL,
    [Description]     VARCHAR (200)  NULL,
    [Manifest]        VARCHAR (8000) NULL,
    [IsApplied]       BIT            CONSTRAINT [DF_SoftwareUpdates_IsApplied] DEFAULT (0) NULL,
    [NoOfUserUpdated] INT            CONSTRAINT [DF_tbl_SoftwareUpdates_NoOfUserUpdated] DEFAULT (0) NULL,
    CONSTRAINT [PK_tbl_SoftwareUpdates] PRIMARY KEY CLUSTERED ([PatchVersion] ASC)
);

