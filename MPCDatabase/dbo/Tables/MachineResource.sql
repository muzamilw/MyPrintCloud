CREATE TABLE [dbo].[MachineResource] (
    [Id]         BIGINT           IDENTITY (1, 1) NOT NULL,
    [MachineId]  INT              NULL,
    [ResourceId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_tbl_machine_resource] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_machines_tbl_machine_resource] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([MachineId])
);

