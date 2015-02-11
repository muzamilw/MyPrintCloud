CREATE TABLE [dbo].[JobStatus] (
    [StatusId]      INT          CONSTRAINT [DF__tbl_job_s__Statu__38B96646] DEFAULT (0) NOT NULL,
    [Description]   VARCHAR (50) NOT NULL,
    [HtmlColorCode] VARCHAR (53) CONSTRAINT [DF_tbl_job_statuses_OleColorCode] DEFAULT ('#ffffff') NULL,
    CONSTRAINT [PK_tbl_job_statuses] PRIMARY KEY CLUSTERED ([StatusId] ASC)
);

