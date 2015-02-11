CREATE TABLE [dbo].[VoucherDetail] (
    [VoucherDetailId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [VoucherId]       BIGINT        NULL,
    [DebitAccount]    INT           CONSTRAINT [DF__tbl_vouch__Debit__2E5BD364] DEFAULT ((0)) NULL,
    [Debit]           FLOAT (53)    CONSTRAINT [DF__tbl_vouch__Debit__2F4FF79D] DEFAULT ((0)) NULL,
    [Credit]          FLOAT (53)    CONSTRAINT [DF__tbl_vouch__Credi__30441BD6] DEFAULT ((0)) NULL,
    [Description]     VARCHAR (255) NULL,
    [CreditAccount]   INT           CONSTRAINT [DF__tbl_vouch__Credi__3138400F] DEFAULT ((0)) NULL,
    [DepartmentId]    INT           NULL,
    CONSTRAINT [PK_tbl_voucherdetail] PRIMARY KEY CLUSTERED ([VoucherDetailId] ASC)
);

