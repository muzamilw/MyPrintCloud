CREATE TABLE [dbo].[Status] (
    [StatusId]    SMALLINT       IDENTITY (1, 1) NOT NULL,
    [StatusName]  NVARCHAR (100) NULL,
    [Description] VARCHAR (100)  NULL,
    [StatusType]  INT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tbl_EstimateStatus] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

