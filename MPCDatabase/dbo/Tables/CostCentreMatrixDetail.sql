CREATE TABLE [dbo].[CostCentreMatrixDetail] (
    [Id]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [MatrixId] INT          CONSTRAINT [DF__tbl_costc__Matri__5DCAEF64] DEFAULT (0) NOT NULL,
    [Value]    VARCHAR (50) CONSTRAINT [DF__tbl_costc__Value__5EBF139D] DEFAULT ('0') NULL,
    CONSTRAINT [PK_tbl_costcentrematrixdetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

