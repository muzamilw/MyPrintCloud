CREATE TABLE [dbo].[UserAchievedTarget] (
    [AchievedTargetId] INT        CONSTRAINT [DF__tbl_usera__Achie__17786E0C] DEFAULT (0) NOT NULL,
    [TargetId]         INT        NULL,
    [SystemUserId]     INT        NULL,
    [EstimateId]       INT        NULL,
    [CustomerId]       INT        NULL,
    [Amount]           FLOAT (53) NULL,
    [Status]           SMALLINT   NULL,
    CONSTRAINT [PK_tbl_userachievedtarget] PRIMARY KEY CLUSTERED ([AchievedTargetId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [TargetID]
    ON [dbo].[UserAchievedTarget]([TargetId] ASC);

