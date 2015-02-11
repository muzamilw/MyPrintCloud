CREATE TABLE [dbo].[ReportCategory] (
    [CategoryId]   INT          IDENTITY (1, 1) NOT NULL,
    [CategoryName] VARCHAR (50) NULL,
    [Description]  VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_reportcategory] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

