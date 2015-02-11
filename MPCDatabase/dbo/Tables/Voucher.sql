CREATE TABLE [dbo].[Voucher] (
    [VoucherId]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [VoucherDate]    DATETIME      NULL,
    [Description]    VARCHAR (250) NULL,
    [Reference]      VARCHAR (50)  NULL,
    [VoucherType]    VARCHAR (5)   NULL,
    [InvoiceType]    VARCHAR (5)   NULL,
    [TotalAmount]    FLOAT (53)    CONSTRAINT [DF__tbl_vouch__Total__24D2692A] DEFAULT ((0)) NOT NULL,
    [CSType]         VARCHAR (5)   NULL,
    [CSCode]         INT           CONSTRAINT [DF__tbl_vouch__CSCod__25C68D63] DEFAULT ((0)) NULL,
    [UserId]         INT           CONSTRAINT [DF__tbl_vouch__UserI__26BAB19C] DEFAULT ((0)) NOT NULL,
    [RIPId]          INT           CONSTRAINT [DF__tbl_vouch__RIPID__27AED5D5] DEFAULT ((0)) NULL,
    [RIPType]        VARCHAR (5)   NULL,
    [ITEMId]         INT           CONSTRAINT [DF__tbl_vouch__ITEMI__28A2FA0E] DEFAULT ((0)) NULL,
    [PaymentMethod]  VARCHAR (15)  NULL,
    [Balance]        FLOAT (53)    CONSTRAINT [DF__tbl_vouch__Balan__29971E47] DEFAULT ((0)) NULL,
    [Reconciled]     VARCHAR (1)   CONSTRAINT [DF__tbl_vouch__Recon__2A8B4280] DEFAULT ('-') NULL,
    [reconciledDate] DATETIME      NULL,
    [SystemSiteId]   INT           NULL,
    CONSTRAINT [PK_tbl_voucher] PRIMARY KEY CLUSTERED ([VoucherId] ASC)
);

