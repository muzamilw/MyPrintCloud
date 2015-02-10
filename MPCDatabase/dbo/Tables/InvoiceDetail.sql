CREATE TABLE [dbo].[InvoiceDetail] (
    [InvoiceDetailId] INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceId]       BIGINT        CONSTRAINT [DF__tbl_invoi__Invoi__6501FCD8] DEFAULT ((0)) NOT NULL,
    [DetailType]      INT           CONSTRAINT [DF__tbl_invoi__Detai__65F62111] DEFAULT ((0)) NOT NULL,
    [ItemId]          BIGINT        CONSTRAINT [DF__tbl_invoi__ItemI__66EA454A] DEFAULT ((0)) NULL,
    [InvoiceTitle]    VARCHAR (150) NULL,
    [NominalCode]     INT           CONSTRAINT [DF__tbl_invoi__Nomin__67DE6983] DEFAULT ((0)) NOT NULL,
    [ItemCharge]      FLOAT (53)    CONSTRAINT [DF__tbl_invoi__ItemC__68D28DBC] DEFAULT ((0)) NOT NULL,
    [Quantity]        FLOAT (53)    CONSTRAINT [DF__tbl_invoi__Quant__69C6B1F5] DEFAULT ((0)) NOT NULL,
    [ItemTaxValue]    FLOAT (53)    CONSTRAINT [DF__tbl_invoi__ItemT__6ABAD62E] DEFAULT ((0)) NOT NULL,
    [FlagId]          INT           CONSTRAINT [DF_tbl_invoicedetails_FlagID] DEFAULT ((0)) NOT NULL,
    [DepartmentId]    INT           NULL,
    [Description]     TEXT          NULL,
    [ItemType]        INT           NULL,
    [TaxId]           INT           NULL,
    CONSTRAINT [PK_tbl_invoicedetails] PRIMARY KEY CLUSTERED ([InvoiceDetailId] ASC),
    CONSTRAINT [FK_tbl_invoicedetails_tbl_invoices] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId]),
    CONSTRAINT [FK_tbl_invoicedetails_tbl_items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId])
);


GO
ALTER TABLE [dbo].[InvoiceDetail] NOCHECK CONSTRAINT [FK_tbl_invoicedetails_tbl_invoices];

