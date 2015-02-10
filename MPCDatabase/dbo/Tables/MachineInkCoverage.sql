CREATE TABLE [dbo].[MachineInkCoverage] (
    [Id]                   BIGINT IDENTITY (1, 1) NOT NULL,
    [SideInkOrder]         INT    NULL,
    [SideInkOrderCoverage] INT    NULL,
    [MachineId]            INT    NULL,
    CONSTRAINT [PK_tbl_machine_ink_coverage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_machines_tbl_machine_ink_coverage] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([MachineId])
);

