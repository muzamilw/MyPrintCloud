CREATE TABLE [dbo].[ChartOfAccount] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [AccountNo]           VARCHAR (20)  NULL,
    [Name]                VARCHAR (100) NOT NULL,
    [OpeningBalance]      FLOAT (53)    CONSTRAINT [DF__tbl_chart__Openi__34C8D9D1] DEFAULT (0.000) NOT NULL,
    [OpeningBalanceType]  SMALLINT      NULL,
    [TypeId]              INT           CONSTRAINT [DF__tbl_chart__TypeI__35BCFE0A] DEFAULT (0) NOT NULL,
    [SubTypeId]           INT           NULL,
    [Description]         VARCHAR (255) NULL,
    [Nature]              SMALLINT      NULL,
    [IsActive]            SMALLINT      CONSTRAINT [DF__tbl_chart__IsAct__36B12243] DEFAULT (1) NULL,
    [IsFixed]             SMALLINT      CONSTRAINT [DF__tbl_chart__IsFix__37A5467C] DEFAULT (0) NULL,
    [LastActivityDate]    DATETIME      NULL,
    [IsForReconciliation] SMALLINT      CONSTRAINT [DF__tbl_chart__IsFor__38996AB5] DEFAULT (0) NOT NULL,
    [Balance]             FLOAT (53)    CONSTRAINT [DF__tbl_chart__Balan__398D8EEE] DEFAULT (0.000) NOT NULL,
    [IsRead]              BIT           NULL,
    [SystemSiteId]        INT           NULL,
    [UserDomainKey]       INT           CONSTRAINT [DF_tbl_chartofaccount_UserDomainKey] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tbl_chartofaccount] PRIMARY KEY CLUSTERED ([Id] ASC)
);

