﻿CREATE TABLE [dbo].[Purchase] (
    [PurchaseId]               INT             IDENTITY (1, 1) NOT NULL,
    [Code]                     VARCHAR (50)    NULL,
    [date_Purchase]            DATETIME        NULL,
    [SupplierId]               INT             CONSTRAINT [DF__tbl_purch__Suppl__74994623] DEFAULT (0) NULL,
    [ContactId]                INT             CONSTRAINT [DF__tbl_purch__Conta__758D6A5C] DEFAULT (0) NOT NULL,
    [SupplierContactCompany]   VARCHAR (50)    NOT NULL,
    [SupplierContactAddressID] INT             CONSTRAINT [DF__tbl_purch__Suppl__76818E95] DEFAULT (0) NOT NULL,
    [TotalPrice]               FLOAT (53)      CONSTRAINT [DF__tbl_purch__Total__7775B2CE] DEFAULT (0) NULL,
    [UserID]                   INT             CONSTRAINT [DF__tbl_purch__UserI__7869D707] DEFAULT (0) NULL,
    [JobID]                    INT             NULL,
    [RefNo]                    VARCHAR (50)    NULL,
    [Comments]                 NVARCHAR (2000) NULL,
    [FootNote]                 NVARCHAR (2000) NULL,
    [UserNotes]                TEXT            NULL,
    [NotesUpdateDateTime]      DATETIME        NULL,
    [NotesUpdatedByUserId]     INT             NULL,
    [isproduct]                INT             NULL,
    [Status]                   INT             NULL,
    [LockedBy]                 INT             CONSTRAINT [DF__tbl_purch__Locke__7A521F79] DEFAULT (0) NULL,
    [TotalTax]                 FLOAT (53)      CONSTRAINT [DF__tbl_purch__Total__7B4643B2] DEFAULT (0) NULL,
    [Discount]                 FLOAT (53)      CONSTRAINT [DF__tbl_purch__Disco__7C3A67EB] DEFAULT (0) NULL,
    [discountType]             INT             NULL,
    [GrandTotal]               FLOAT (53)      CONSTRAINT [DF__tbl_purch__Grand__7D2E8C24] DEFAULT (0) NULL,
    [NetTotal]                 FLOAT (53)      NULL,
    [CreatedBy]                INT             CONSTRAINT [DF__tbl_purch__Creat__7E22B05D] DEFAULT (0) NULL,
    [LastChangedBy]            INT             CONSTRAINT [DF__tbl_purch__LastC__7F16D496] DEFAULT (0) NULL,
    [FlagID]                   INT             CONSTRAINT [DF__tbl_purch__FlagI__000AF8CF] DEFAULT (0) NULL,
    [SystemSiteId]             INT             NULL,
    [IsRead]                   BIT             CONSTRAINT [DF_tbl_purchase_IsRead] DEFAULT (0) NULL,
    [IsPrinted]                BIT             NULL,
    [XeroAccessCode]           VARCHAR (50)    NULL,
    CONSTRAINT [PK_tbl_purchase] PRIMARY KEY CLUSTERED ([PurchaseId] ASC)
);

