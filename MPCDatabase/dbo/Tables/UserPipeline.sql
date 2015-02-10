CREATE TABLE [dbo].[UserPipeline] (
    [PipeLineId]   INT      IDENTITY (1, 1) NOT NULL,
    [TargetId]     INT      NULL,
    [SystemUserId] INT      NULL,
    [EstimateId]   INT      NULL,
    [CustomerId]   INT      NULL,
    [Status]       SMALLINT CONSTRAINT [DF__tbl_userp__Statu__1A54DAB7] DEFAULT (0) NULL,
    CONSTRAINT [PK_tbl_userpipeline] PRIMARY KEY CLUSTERED ([PipeLineId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [TargetID]
    ON [dbo].[UserPipeline]([TargetId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [PipeLineID]
    ON [dbo].[UserPipeline]([PipeLineId] ASC);

