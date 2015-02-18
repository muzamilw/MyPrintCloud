CREATE TABLE [dbo].[PipeLineSource] (
    [SourceId]    INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_pipeline_source] PRIMARY KEY CLUSTERED ([SourceId] ASC)
);

