CREATE TABLE [dbo].[PipelineDeduction] (
    [PipeLineDeductionId] INT IDENTITY (1, 1) NOT NULL,
    [Months]              INT NULL,
    [Percentage]          INT NULL,
    CONSTRAINT [PK_tbl_pipeline_deduction] PRIMARY KEY CLUSTERED ([PipeLineDeductionId] ASC)
);

