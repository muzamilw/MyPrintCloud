CREATE TABLE [dbo].[PurchaseDetail] (
    [PurchaseDetailId] INT           IDENTITY (1, 1) NOT NULL,
    [ItemId]           INT           NULL,
    [quantity]         FLOAT (53)    NULL,
    [PurchaseId]       INT           CONSTRAINT [DF__tbl_purch__Purch__02E7657A] DEFAULT (0) NULL,
    [price]            FLOAT (53)    CONSTRAINT [DF__tbl_purch__price__03DB89B3] DEFAULT (0) NULL,
    [packqty]          INT           NULL,
    [ItemCode]         VARCHAR (50)  NULL,
    [ServiceDetail]    TEXT          NULL,
    [ItemName]         VARCHAR (255) NULL,
    [TaxId]            INT           NULL,
    [TotalPrice]       FLOAT (53)    NULL,
    [Discount]         FLOAT (53)    CONSTRAINT [DF__tbl_purch__Disco__04CFADEC] DEFAULT (0) NULL,
    [NetTax]           FLOAT (53)    CONSTRAINT [DF__tbl_purch__NetTa__05C3D225] DEFAULT (0) NULL,
    [freeitems]        INT           CONSTRAINT [DF__tbl_purch__freei__06B7F65E] DEFAULT (0) NULL,
    [ItemBalance]      FLOAT (53)    CONSTRAINT [DF__tbl_purch__ItemB__07AC1A97] DEFAULT (0) NULL,
    [DepartmentId]     INT           NULL,
    CONSTRAINT [PK_tbl_purchasedetail] PRIMARY KEY CLUSTERED ([PurchaseDetailId] ASC),
    CONSTRAINT [FK_PurchaseID] FOREIGN KEY ([PurchaseId]) REFERENCES [dbo].[Purchase] ([PurchaseId])
);

