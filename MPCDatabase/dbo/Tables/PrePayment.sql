CREATE TABLE [dbo].[PrePayment] (
    [PrePaymentId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerId]         INT           NULL,
    [OrderId]            BIGINT        NULL,
    [Amount]             FLOAT (53)    NULL,
    [PaymentDate]        DATETIME      NOT NULL,
    [PayPalResponseId]   BIGINT        NULL,
    [PaymentMethodId]    INT           NOT NULL,
    [ReferenceCode]      VARCHAR (MAX) NULL,
    [PaymentDescription] VARCHAR (500) NULL,
    CONSTRAINT [PK_tbl_PrePayments] PRIMARY KEY CLUSTERED ([PrePaymentId] ASC),
    CONSTRAINT [FK_tbl_PrePayments_tbl_estimates] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Estimate] ([EstimateId]),
    CONSTRAINT [FK_tbl_PrePayments_tbl_PaymentMethods] FOREIGN KEY ([PaymentMethodId]) REFERENCES [dbo].[PaymentMethod] ([PaymentMethodId]),
    CONSTRAINT [FK_tbl_PrePayments_tbl_PayPalResponses] FOREIGN KEY ([PayPalResponseId]) REFERENCES [dbo].[PayPalResponse] ([PayPalResponseId])
);

