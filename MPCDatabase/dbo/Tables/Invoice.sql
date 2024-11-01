﻿CREATE TABLE [dbo].[Invoice] (
    [InvoiceId]             BIGINT           IDENTITY (1, 1) NOT NULL,
    [InvoiceCode]           VARCHAR (100)    NULL,
    [InvoiceType]           INT              CONSTRAINT [DF__tbl_invoi__Invoi__6D9742D9] DEFAULT ((0)) NULL,
    [InvoiceName]           VARCHAR (255)    NULL,
    [ContactCompanyID]      INT              CONSTRAINT [DF__tbl_invoi__Custo__6E8B6712] DEFAULT ((0)) NULL,
    [ContactId]             INT              CONSTRAINT [DF__tbl_invoi__Conta__6F7F8B4B] DEFAULT ((0)) NULL,
    [ContactCompany]        VARCHAR (50)     NULL,
    [OrderNo]               VARCHAR (50)     NULL,
    [InvoiceStatus]         INT              CONSTRAINT [DF__tbl_invoi__Invoi__7073AF84] DEFAULT ((0)) NULL,
    [InvoiceTotal]          FLOAT (53)       CONSTRAINT [DF__tbl_invoi__Invoi__7167D3BD] DEFAULT ((0)) NULL,
    [InvoiceDate]           DATETIME         NULL,
    [LastUpdatedBy]         UNIQUEIDENTIFIER NULL,
    [CreationDate]          DATETIME         NULL,
    [CreatedBy]             UNIQUEIDENTIFIER NULL,
    [AccountNumber]         VARCHAR (50)     CONSTRAINT [DF__tbl_invoi__Accou__74444068] DEFAULT ('0') NULL,
    [Terms]                 TEXT             NULL,
    [InvoicePostingDate]    DATETIME         NULL,
    [InvoicePostedBy]       UNIQUEIDENTIFIER NULL,
    [LockedBy]              UNIQUEIDENTIFIER NULL,
    [AddressId]             INT              CONSTRAINT [DF__tbl_invoi__Addre__762C88DA] DEFAULT ((0)) NULL,
    [IsArchive]             BIT              CONSTRAINT [DF__tbl_invoi__IsArc__7720AD13] DEFAULT ((0)) NULL,
    [TaxValue]              FLOAT (53)       CONSTRAINT [DF__tbl_invoi__TaxVa__7814D14C] DEFAULT ((0)) NULL,
    [GrandTotal]            FLOAT (53)       CONSTRAINT [DF__tbl_invoi__Grand__7908F585] DEFAULT ((0)) NULL,
    [FlagID]                INT              CONSTRAINT [DF__tbl_invoi__FlagI__79FD19BE] DEFAULT ((0)) NULL,
    [UserNotes]             TEXT             NULL,
    [NotesUpdateDateTime]   DATETIME         NULL,
    [NotesUpdatedByUserID]  INT              NULL,
    [SystemSiteId]          INT              NULL,
    [EstimateId]            INT              NULL,
    [IsRead]                BIT              CONSTRAINT [DF_tbl_invoices_IsRead] DEFAULT ((0)) NULL,
    [IsProformaInvoice]     BIT              CONSTRAINT [DF_tbl_invoices_IsProformaInvoice] DEFAULT ((0)) NULL,
    [IsPrinted]             BIT              NULL,
    [LastUpdateDate]        DATETIME         NULL,
    [ReportSignedBy]        UNIQUEIDENTIFIER NULL,
    [ReportLastPrintedDate] DATETIME         NULL,
    [HeadNotes]             NVARCHAR (2000)  NULL,
    [FootNotes]             NVARCHAR (2000)  NULL,
    [XeroAccessCode]        VARCHAR (50)     NULL,
    [OrganisationId]        BIGINT           NULL,
    CONSTRAINT [PK_tbl_invoices] PRIMARY KEY CLUSTERED ([InvoiceId] ASC)
);

