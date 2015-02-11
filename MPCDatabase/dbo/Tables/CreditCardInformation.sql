CREATE TABLE [dbo].[CreditCardInformation] (
    [TransactionId]     BIGINT       NOT NULL,
    [VoucherId]         BIGINT       CONSTRAINT [DF__tbl_credi__Vouch__208CD6FA] DEFAULT (270592649888) NOT NULL,
    [CardHolder]        VARCHAR (50) NOT NULL,
    [BillingAddress]    VARCHAR (50) NULL,
    [City]              VARCHAR (50) NULL,
    [State]             VARCHAR (50) NULL,
    [Zip]               VARCHAR (50) NULL,
    [Country]           VARCHAR (50) NULL,
    [Comments]          VARCHAR (50) NULL,
    [CardNumber]        VARCHAR (50) NOT NULL,
    [ExpireDate]        DATETIME     NULL,
    [AuthorizationCode] VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_creditcardinformation] PRIMARY KEY CLUSTERED ([TransactionId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [index_TransactionID]
    ON [dbo].[CreditCardInformation]([TransactionId] ASC);

