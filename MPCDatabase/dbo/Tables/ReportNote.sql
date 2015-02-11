CREATE TABLE [dbo].[ReportNote] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [FootNotes]          TEXT           NULL,
    [HeadNotes]          TEXT           NULL,
    [AdvertitorialNotes] TEXT           NULL,
    [UserId]             INT            NULL,
    [ReportCategoryId]   INT            NULL,
    [SystemSiteId]       INT            CONSTRAINT [DF__tbl_repor__Depar__0C70CFB4] DEFAULT (0) NOT NULL,
    [ReportBanner]       NVARCHAR (400) NULL,
    [ReportTitle]        VARCHAR (200)  NULL,
    [BannerAbsolutePath] VARCHAR (300)  NULL,
    [isDefault]          BIT            NULL,
    CONSTRAINT [PK_tbl_report_notes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_report_notes_tbl_reportcategory] FOREIGN KEY ([ReportCategoryId]) REFERENCES [dbo].[ReportCategory] ([CategoryId])
);

