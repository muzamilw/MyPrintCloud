CREATE TABLE [dbo].[AccountDefault] (
    [DefaultId]          INT      IDENTITY (1, 1) NOT NULL,
    [StartFinancialYear] DATETIME NULL,
    [EndFinancialYear]   DATETIME NULL,
    [Bank]               INT      CONSTRAINT [DF__tbl_accoun__Bank__76CBA758] DEFAULT (0) NOT NULL,
    [Sales]              INT      CONSTRAINT [DF__tbl_accou__Sales__77BFCB91] DEFAULT (0) NOT NULL,
    [Till]               INT      CONSTRAINT [DF__tbl_accoun__Till__78B3EFCA] DEFAULT (0) NOT NULL,
    [Purchase]           INT      CONSTRAINT [DF__tbl_accou__Purch__79A81403] DEFAULT (0) NOT NULL,
    [Debitor]            INT      CONSTRAINT [DF__tbl_accou__Debit__7A9C383C] DEFAULT (0) NOT NULL,
    [Creditor]           INT      CONSTRAINT [DF__tbl_accou__Credi__7B905C75] DEFAULT (0) NOT NULL,
    [Prepayment]         INT      CONSTRAINT [DF__tbl_accou__Prepa__7C8480AE] DEFAULT (0) NOT NULL,
    [VATonSale]          INT      CONSTRAINT [DF__tbl_accou__VATon__7D78A4E7] DEFAULT (0) NOT NULL,
    [VATonPurchase]      INT      CONSTRAINT [DF__tbl_accou__VATon__7E6CC920] DEFAULT (0) NOT NULL,
    [DiscountAllowed]    INT      CONSTRAINT [DF__tbl_accou__Disco__7F60ED59] DEFAULT (0) NOT NULL,
    [DiscountTaken]      INT      CONSTRAINT [DF__tbl_accou__Disco__00551192] DEFAULT (0) NOT NULL,
    [OpeningBalance]     INT      CONSTRAINT [DF__tbl_accou__Openi__014935CB] DEFAULT (0) NOT NULL,
    [SystemSiteId]       INT      CONSTRAINT [DF_tbl_accountdefault_SystemSiteID] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_accountdefault] PRIMARY KEY CLUSTERED ([DefaultId] ASC)
);

