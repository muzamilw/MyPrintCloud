CREATE TABLE [dbo].[PaypalPaymentRequest] (
    [Request_ID]  INT            IDENTITY (1, 1) NOT NULL,
    [Order_ID]    INT            NOT NULL,
    [ProductID]   VARCHAR (5000) NULL,
    [Price]       DECIMAL (18)   NOT NULL,
    [RequestDate] DATETIME       NOT NULL,
    [Status]      INT            NOT NULL,
    CONSTRAINT [PK_tblPaypalPaymentRequest] PRIMARY KEY CLUSTERED ([Request_ID] ASC)
);

