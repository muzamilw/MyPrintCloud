CREATE TABLE [dbo].[CostcentreWorkInstructionsChoice] (
    [Id]            BIGINT       IDENTITY (1, 1) NOT NULL,
    [Choice]        VARCHAR (50) NOT NULL,
    [InstructionId] BIGINT       CONSTRAINT [DF__tbl_costc__Instr__534D60F1] DEFAULT (270592646992) NOT NULL,
    CONSTRAINT [PK_tbl_costcentre_workinstructions_choices] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [tbl_costcentres_instructions_tbl_costcentre_workinstructions_choices] FOREIGN KEY ([InstructionId]) REFERENCES [dbo].[CostcentreInstruction] ([InstructionId])
);

