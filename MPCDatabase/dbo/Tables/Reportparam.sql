CREATE TABLE [dbo].[Reportparam] (
    [ParmId]             INT          IDENTITY (1, 1) NOT NULL,
    [ParmName]           VARCHAR (50) NOT NULL,
    [Caption1]           VARCHAR (50) NOT NULL,
    [ReportId]           INT          CONSTRAINT [DF__tbl_repor__Repor__113584D1] DEFAULT (0) NOT NULL,
    [ControlType]        INT          CONSTRAINT [DF__tbl_report__Type__1229A90A] DEFAULT (0) NOT NULL,
    [ComboTableName]     VARCHAR (50) NULL,
    [ComboIDFieldName]   VARCHAR (50) NULL,
    [ComboTextFieldName] VARCHAR (50) NULL,
    [CriteriaFieldName]  VARCHAR (50) NULL,
    [OrderByFieldName]   VARCHAR (50) NULL,
    [SameAsPArmId]       INT          NULL,
    [Caption2]           VARCHAR (50) NULL,
    [Operator]           INT          CONSTRAINT [DF_tbl_reportparms_Operator] DEFAULT (0) NULL,
    [LogicalOperator]    INT          CONSTRAINT [DF_tbl_reportparms_LogicalOperator] DEFAULT (0) NULL,
    [DefaultValue1]      VARCHAR (50) CONSTRAINT [DF_tbl_reportparms_DefaultValue1] DEFAULT (0) NULL,
    [DefaultValue2]      VARCHAR (50) CONSTRAINT [DF_tbl_reportparms_DefaultValue2] DEFAULT (0) NULL,
    [MinValue]           FLOAT (53)   CONSTRAINT [DF_tbl_reportparms_MinValue] DEFAULT (0) NULL,
    [MaxValue]           FLOAT (53)   CONSTRAINT [DF_tbl_reportparms_MaxValue] DEFAULT (0) NULL,
    [FilterType]         INT          NULL,
    [SortOrder]          INT          NULL,
    CONSTRAINT [PK_tbl_reportparms] PRIMARY KEY CLUSTERED ([ParmId] ASC)
);

