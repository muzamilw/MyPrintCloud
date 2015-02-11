CREATE TABLE [dbo].[CostcentreInstruction] (
    [InstructionId]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [Instruction]      VARCHAR (255) NULL,
    [CostCentreId]     BIGINT        NULL,
    [CostCenterOption] INT           NULL,
    CONSTRAINT [PK_tbl_costcentre_instructions] PRIMARY KEY CLUSTERED ([InstructionId] ASC),
    CONSTRAINT [tbl_costcentres_tbl_costcentre_instructions] FOREIGN KEY ([CostCentreId]) REFERENCES [dbo].[CostCentre] ([CostCentreId])
);


GO
CREATE NONCLUSTERED INDEX [CostCentreID]
    ON [dbo].[CostcentreInstruction]([CostCentreId] ASC);


GO
CREATE NONCLUSTERED INDEX [InstructionID]
    ON [dbo].[CostcentreInstruction]([InstructionId] ASC);

