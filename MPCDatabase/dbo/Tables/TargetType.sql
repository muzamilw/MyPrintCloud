CREATE TABLE [dbo].[TargetType] (
    [TargetTypeId]          INT           IDENTITY (1, 1) NOT NULL,
    [TargetTypeDescription] VARCHAR (100) NULL,
    [Criteria]              VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_targettype] PRIMARY KEY CLUSTERED ([TargetTypeId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [TargetTypeID]
    ON [dbo].[TargetType]([TargetTypeId] ASC);

