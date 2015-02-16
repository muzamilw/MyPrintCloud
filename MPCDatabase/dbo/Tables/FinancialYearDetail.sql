CREATE TABLE [dbo].[FinancialYearDetail] (
    [Id]              INT        IDENTITY (1, 1) NOT NULL,
    [FinancialYearId] INT        NULL,
    [AccountNo]       INT        NULL,
    [OpenningBalance] FLOAT (53) NULL,
    [ClosingBalance]  FLOAT (53) NULL,
    CONSTRAINT [PK_tbl_financialyeardetail] PRIMARY KEY CLUSTERED ([Id] ASC)
);

