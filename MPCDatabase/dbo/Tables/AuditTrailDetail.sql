CREATE TABLE [dbo].[AuditTrailDetail] (
    [VoucherDetailID] BIGINT        CONSTRAINT [DF__tbl_audit__Vouch__29572725] DEFAULT (9221964782240021712) NOT NULL,
    [VoucherId]       BIGINT        CONSTRAINT [DF__tbl_audit__Vouch__2A4B4B5E] DEFAULT (270592654016) NOT NULL,
    [DebitAccount]    INT           CONSTRAINT [DF__tbl_audit__Debit__2B3F6F97] DEFAULT (0) NOT NULL,
    [Debit]           FLOAT (53)    CONSTRAINT [DF__tbl_audit__Debit__2C3393D0] DEFAULT (0) NOT NULL,
    [Credit]          FLOAT (53)    CONSTRAINT [DF__tbl_audit__Credi__2D27B809] DEFAULT (0) NOT NULL,
    [Description]     VARCHAR (255) NOT NULL,
    [CreditAccount]   INT           CONSTRAINT [DF__tbl_audit__Credi__2E1BDC42] DEFAULT (0) NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Index_VoucherDetailID]
    ON [dbo].[AuditTrailDetail]([VoucherDetailID] ASC);

