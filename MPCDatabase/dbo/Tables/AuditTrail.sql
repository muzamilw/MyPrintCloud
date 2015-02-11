CREATE TABLE [dbo].[AuditTrail] (
    [VoucherId]      BIGINT        CONSTRAINT [DF__tbl_audit__Vouch__20C1E124] DEFAULT (270592658736) NOT NULL,
    [VoucherDate]    DATETIME      NULL,
    [Description]    VARCHAR (250) NOT NULL,
    [Reference]      VARCHAR (50)  NOT NULL,
    [VoucherType]    VARCHAR (5)   NULL,
    [InvoiceType]    VARCHAR (5)   NULL,
    [TotalAmount]    FLOAT (53)    CONSTRAINT [DF__tbl_audit__Total__21B6055D] DEFAULT (0) NOT NULL,
    [CSType]         VARCHAR (5)   NULL,
    [CSCode]         INT           CONSTRAINT [DF__tbl_audit__CSCod__22AA2996] DEFAULT (0) NULL,
    [UserId]         INT           CONSTRAINT [DF__tbl_audit__UserI__239E4DCF] DEFAULT (0) NOT NULL,
    [RIPId]          INT           CONSTRAINT [DF__tbl_audit__RIPID__24927208] DEFAULT (0) NULL,
    [RIPType]        VARCHAR (5)   NULL,
    [ITEMId]         INT           CONSTRAINT [DF__tbl_audit__ITEMI__25869641] DEFAULT (0) NULL,
    [PaymentMethod]  VARCHAR (5)   NULL,
    [Balance]        FLOAT (53)    CONSTRAINT [DF__tbl_audit__Balan__267ABA7A] DEFAULT (0) NOT NULL,
    [Reconciled]     VARCHAR (1)   CONSTRAINT [DF__tbl_audit__Recon__276EDEB3] DEFAULT ('-') NOT NULL,
    [reconciledDate] DATETIME      NULL
);

