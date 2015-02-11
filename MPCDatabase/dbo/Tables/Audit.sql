CREATE TABLE [dbo].[Audit] (
    [AuditNo]            BIGINT        IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceType]        VARCHAR (50)  NOT NULL,
    [InvoiceNo]          INT           CONSTRAINT [DF__tbl_audit__Invoi__173876EA] DEFAULT (0) NULL,
    [Account]            VARCHAR (50)  NULL,
    [Detail]             VARCHAR (255) NULL,
    [TransactionDate]    DATETIME      NULL,
    [Reference]          VARCHAR (50)  NULL,
    [Net]                FLOAT (53)    CONSTRAINT [DF__tbl_audit__Net__182C9B23] DEFAULT (0) NOT NULL,
    [Tax]                FLOAT (53)    CONSTRAINT [DF__tbl_audit__Tax__1920BF5C] DEFAULT (0) NOT NULL,
    [Paid]               INT           CONSTRAINT [DF__tbl_audit__Paid__1A14E395] DEFAULT (0) NULL,
    [AmountPaid]         FLOAT (53)    CONSTRAINT [DF__tbl_audit__Amoun__1B0907CE] DEFAULT (0) NOT NULL,
    [Reconciled]         VARCHAR (1)   CONSTRAINT [DF__tbl_audit__Recon__1BFD2C07] DEFAULT ('-') NOT NULL,
    [BankReconciledDate] DATETIME      NULL,
    [TransactionUser]    VARCHAR (50)  NULL,
    [VAT]                VARCHAR (50)  CONSTRAINT [DF__tbl_audit__VAT__1CF15040] DEFAULT ('-') NULL,
    [Nominal]            INT           CONSTRAINT [DF__tbl_audit__Nomin__1DE57479] DEFAULT (0) NOT NULL,
    [SystemSiteId]       INT           NULL,
    CONSTRAINT [PK_tbl_audit] PRIMARY KEY CLUSTERED ([AuditNo] ASC)
);

