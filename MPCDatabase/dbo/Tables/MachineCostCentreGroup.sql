CREATE TABLE [dbo].[MachineCostCentreGroup] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [MachineId]         INT NULL,
    [CostCentreGroupId] INT NULL,
    CONSTRAINT [PK_tbl_machine_costcentre_groups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_machines_tbl_machine_costcentre_groups] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([MachineId])
);

