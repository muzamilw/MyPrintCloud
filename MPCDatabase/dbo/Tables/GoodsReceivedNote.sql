﻿CREATE TABLE [dbo].[GoodsReceivedNote] (
    [GoodsReceivedId] INT             IDENTITY (1, 1) NOT NULL,
    [PurchaseId]      INT             NULL,
    [date_Received]   DATETIME        NULL,
    [SupplierId]      INT             CONSTRAINT [DF__tbl_goods__Suppl__52E34C9D] DEFAULT (0) NULL,
    [TotalPrice]      FLOAT (53)      CONSTRAINT [DF__tbl_goods__Total__53D770D6] DEFAULT (0) NULL,
    [UserId]          INT             CONSTRAINT [DF__tbl_goods__UserI__54CB950F] DEFAULT (0) NULL,
    [Address]         VARCHAR (30)    NULL,
    [City]            VARCHAR (30)    NULL,
    [State]           VARCHAR (30)    NULL,
    [PostalCode]      VARCHAR (30)    NULL,
    [Country]         VARCHAR (30)    NULL,
    [ContactId]       INT             NULL,
    [RefNo]           VARCHAR (30)    NULL,
    [Comments]        NVARCHAR (2000) NULL,
    [UserNotes]       TEXT            NULL,
    [isProduct]       INT             NULL,
    [Tel1]            VARCHAR (30)    NULL,
    [code]            VARCHAR (50)    NOT NULL,
    [Discount]        FLOAT (53)      NULL,
    [TotalTax]        FLOAT (53)      NULL,
    [grandTotal]      FLOAT (53)      NULL,
    [NetTotal]        FLOAT (53)      NULL,
    [discountType]    INT             NULL,
    [Status]          INT             NULL,
    [CreatedBy]       INT             NULL,
    [LastChangedBy]   INT             NOT NULL,
    [FlagId]          INT             CONSTRAINT [DF__tbl_goods__FlagI__56B3DD81] DEFAULT (0) NOT NULL,
    [LockedBy]        INT             CONSTRAINT [DF__tbl_goods__Locke__57A801BA] DEFAULT (0) NOT NULL,
    [SystemSiteId]    INT             NULL,
    [IsRead]          BIT             CONSTRAINT [DF_tbl_goodsreceivednote_IsRead] DEFAULT (0) NULL,
    [DeliveryDate]    DATETIME        NULL,
    [Reference1]      VARCHAR (50)    NULL,
    [Reference2]      VARCHAR (50)    NULL,
    [CarrierId]       INT             NULL,
    CONSTRAINT [PK_tbl_goodsreceivednote] PRIMARY KEY CLUSTERED ([GoodsReceivedId] ASC)
);
