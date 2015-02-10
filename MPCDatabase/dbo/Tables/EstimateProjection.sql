CREATE TABLE [dbo].[EstimateProjection] (
    [ProjectionId]         INT           IDENTITY (1, 1) NOT NULL,
    [ContactCompanyId]     INT           NULL,
    [Amount]               FLOAT (53)    NULL,
    [SuccessChanceId]      INT           NULL,
    [SalesPerson]          INT           NULL,
    [EstimateDate]         DATETIME      NULL,
    [IsProjectionAlarm]    BIT           CONSTRAINT [DF_tbl_estimate_projection_IsProjectionAlarm] DEFAULT (1) NULL,
    [AlarmDate]            DATETIME      NULL,
    [AlarmTime]            DATETIME      NULL,
    [Notes]                VARCHAR (100) NULL,
    [IsIncludedInPipeLine] SMALLINT      CONSTRAINT [DF__tbl_estim__IsInc__18B6AB08] DEFAULT (1) NULL,
    [SourceId]             INT           CONSTRAINT [DF_tbl_estimate_projection_SourceID] DEFAULT (1) NULL,
    [ProductId]            INT           CONSTRAINT [DF_tbl_estimate_projection_ProductID] DEFAULT (1) NULL,
    [IsLocked]             SMALLINT      CONSTRAINT [DF__tbl_estim__IsLoc__19AACF41] DEFAULT (0) NULL,
    [LockedBy]             INT           CONSTRAINT [DF_tbl_estimate_projection_LockedBy] DEFAULT (0) NULL,
    CONSTRAINT [PK_tbl_estimate_projection] PRIMARY KEY CLUSTERED ([ProjectionId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For Sales PipeLine search', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EstimateProjection', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For sales pipeline search', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EstimateProjection', @level2type = N'COLUMN', @level2name = N'SourceId';

