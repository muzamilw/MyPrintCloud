CREATE TABLE [dbo].[Ledger] (
    [LedgerId]           BIGINT        NULL,
    [VoucherId]          BIGINT        NULL,
    [VoucherDescription] VARCHAR (255) NULL,
    [Reference]          VARCHAR (50)  NULL,
    [Custcode]           BIGINT        NULL,
    [CodeType]           VARCHAR (1)   NULL,
    [VoucherDate]        DATETIME      NULL,
    [UserId]             INT           NULL,
    [DebitAccountId]     INT           NULL,
    [CreditAccountId]    INT           NULL,
    [Debit]              FLOAT (53)    NULL,
    [Credit]             FLOAT (53)    NULL
);

