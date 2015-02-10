CREATE TABLE [dbo].[UserTarget] (
    [TargetId]     INT        IDENTITY (1, 1) NOT NULL,
    [TargetTypeId] INT        NULL,
    [SystemUserId] INT        CONSTRAINT [DF__tbl_usert__Syste__21F5FC7F] DEFAULT (0) NOT NULL,
    [TargetFrom]   DATETIME   NULL,
    [TargetTo]     DATETIME   NULL,
    [Amount]       FLOAT (53) NULL,
    [Balance]      FLOAT (53) NULL,
    [IsActive]     SMALLINT   NULL,
    CONSTRAINT [PK_tbl_usertarget] PRIMARY KEY CLUSTERED ([TargetId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [TargetTypeID]
    ON [dbo].[UserTarget]([TargetTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [SystemUserID]
    ON [dbo].[UserTarget]([SystemUserId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [TargetID]
    ON [dbo].[UserTarget]([TargetId] ASC);

