CREATE TABLE [dbo].[Currency] (
    [CurrencyId]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [CurrencyCode] VARCHAR (50) NULL,
    [CurrencyName] VARCHAR (50) NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([CurrencyId] ASC)
);

