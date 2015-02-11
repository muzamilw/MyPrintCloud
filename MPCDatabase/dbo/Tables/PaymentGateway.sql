CREATE TABLE [dbo].[PaymentGateway] (
    [PaymentGatewayId] INT            IDENTITY (1, 1) NOT NULL,
    [BusinessEmail]    VARCHAR (500)  NULL,
    [IdentityToken]    VARCHAR (500)  NULL,
    [isActive]         BIT            CONSTRAINT [DF_tbl_PaymentGateways_isActive] DEFAULT ((0)) NOT NULL,
    [CompanyId]        BIGINT         NULL,
    [PaymentMethodId]  INT            NULL,
    [SecureHash]       NVARCHAR (500) NULL,
    CONSTRAINT [PK_tbl_PaymentGateways] PRIMARY KEY CLUSTERED ([PaymentGatewayId] ASC),
    CONSTRAINT [FK_PaymentGateway_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_tbl_PaymentGateways_tbl_PaymentMethods] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[PaymentMethod] ([PaymentMethodId])
);

