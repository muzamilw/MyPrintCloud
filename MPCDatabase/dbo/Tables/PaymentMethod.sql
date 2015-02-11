CREATE TABLE [dbo].[PaymentMethod] (
    [PaymentMethodId] INT           NOT NULL,
    [MethodName]      VARCHAR (200) NULL,
    [IsActive]        BIT           NULL,
    CONSTRAINT [PK_tbl_PaymentMethods] PRIMARY KEY CLUSTERED ([PaymentMethodId] ASC)
);

