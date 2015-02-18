CREATE TABLE [dbo].[AppVersions] (
    [VersionId]              INT          IDENTITY (1, 1) NOT NULL,
    [CurrentMISVersion]      VARCHAR (50) NULL,
    [CurrentWebStoreVersion] VARCHAR (50) NULL,
    [DBScriptDate]           DATETIME     NULL,
    [DeploymentDate]         DATETIME     NULL,
    [DBScriptTag]            VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_AppVersions] PRIMARY KEY CLUSTERED ([VersionId] ASC)
);

