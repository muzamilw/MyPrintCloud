CREATE TABLE [dbo].[DiscountVoucher] (
    [DiscountVoucherId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [VoucherCode]       VARCHAR (400) NOT NULL,
    [ValidFromDate]     DATETIME      NULL,
    [ValidUptoDate]     DATETIME      NULL,
    [OrderId]           BIGINT        NULL,
    [DiscountRate]      FLOAT (53)    NOT NULL,
    [ConsumedDate]      DATETIME      NULL,
    [IsEnabled]         BIT           CONSTRAINT [DF_tbl_DiscountVouchers_IsEnabled] DEFAULT ((1)) NOT NULL,
    [CreatedDate]       DATETIME      NULL,
    [CompanyId]         BIGINT        NULL,
    CONSTRAINT [PK_tbl_DiscountVouchers] PRIMARY KEY CLUSTERED ([DiscountVoucherId] ASC),
    CONSTRAINT [Unique_VoucherCode] UNIQUE NONCLUSTERED ([VoucherCode] ASC)
);

