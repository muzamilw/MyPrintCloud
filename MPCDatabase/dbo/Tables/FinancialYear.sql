CREATE TABLE [dbo].[FinancialYear] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [StartDate]    DATETIME NULL,
    [EndDate]      DATETIME NULL,
    [SystemSiteId] INT      NULL,
    CONSTRAINT [PK_tbl_financialyears] PRIMARY KEY CLUSTERED ([Id] ASC)
);

