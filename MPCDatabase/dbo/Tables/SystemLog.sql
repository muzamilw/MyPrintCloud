CREATE TABLE [dbo].[SystemLog] (
    [LogId]        INT      IDENTITY (1, 1) NOT NULL,
    [SectionId]    INT      NULL,
    [Id]           INT      NULL,
    [Notes]        TEXT     NULL,
    [LogDate]      DATETIME NULL,
    [LogTime]      DATETIME NULL,
    [SystemUserId] INT      NULL,
    CONSTRAINT [PK_tbl_system_log] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

