CREATE TABLE [dbo].[NominalDetail] (
    [AccountNo]        INT           NULL,
    [BankDescription]  VARCHAR (255) NULL,
    [NoReconciliation] SMALLINT      NULL,
    [BankName]         VARCHAR (255) NULL,
    [BankAddress]      VARCHAR (255) NULL,
    [BankTown]         VARCHAR (50)  NULL,
    [BankCounty]       VARCHAR (50)  NULL,
    [BankPostcode]     VARCHAR (50)  NULL,
    [AccountName]      VARCHAR (255) NULL,
    [AccountNumber]    VARCHAR (50)  NULL,
    [SortCode]         VARCHAR (50)  NULL,
    [ExpiryDate]       DATETIME      NULL,
    [ContactName]      VARCHAR (255) NULL,
    [ContactTel]       VARCHAR (50)  NULL,
    [ContactFax]       VARCHAR (50)  NULL,
    [ContactEmail]     VARCHAR (255) NULL,
    [ContactURL]       VARCHAR (255) NULL,
    [SystemSiteId]     INT           NULL,
    [IBN]              VARCHAR (50)  NULL
);

