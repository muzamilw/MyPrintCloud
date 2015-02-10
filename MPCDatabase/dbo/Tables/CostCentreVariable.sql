CREATE TABLE [dbo].[CostCentreVariable] (
    [VarId]               INT           IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    [RefTableName]        VARCHAR (100) NULL,
    [RefFieldName]        VARCHAR (100) NULL,
    [CriteriaFieldName]   VARCHAR (100) NULL,
    [Criteria]            VARCHAR (100) NULL,
    [CategoryId]          INT           CONSTRAINT [DF__tbl_costc__Categ__14270015] DEFAULT (0) NOT NULL,
    [IsCriteriaUsed]      VARCHAR (1)   CONSTRAINT [DF__tbl_costc__IsCri__151B244E] DEFAULT ('0') NULL,
    [Type]                SMALLINT      CONSTRAINT [DF__tbl_costce__Type__160F4887] DEFAULT (0) NOT NULL,
    [PropertyType]        INT           NULL,
    [VariableDescription] TEXT          NULL,
    [VariableValue]       FLOAT (53)    NULL,
    [SystemSiteId]        INT           CONSTRAINT [DF__tbl_costc__Syste__17F790F9] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_costcentrevariables] PRIMARY KEY CLUSTERED ([VarId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [CategoryID]
    ON [dbo].[CostCentreVariable]([CategoryId] ASC);

