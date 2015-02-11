CREATE TABLE [dbo].[UserReport] (
    [UserReportId]     INT      IDENTITY (1, 1) NOT NULL,
    [SystemUserId]     INT      CONSTRAINT [DF__tbl_userr__Syste__1D314762] DEFAULT (0) NOT NULL,
    [ReportId]         INT      CONSTRAINT [DF__tbl_userr__Repor__1E256B9B] DEFAULT (0) NOT NULL,
    [IsDefault]        SMALLINT CONSTRAINT [DF__tbl_userr__IsDef__1F198FD4] DEFAULT (0) NOT NULL,
    [ReportCategoryId] INT      CONSTRAINT [DF_tbl_userreports_CategoryID] DEFAULT (0) NULL,
    CONSTRAINT [PK_tbl_userreports] PRIMARY KEY CLUSTERED ([UserReportId] ASC)
);

