CREATE TABLE [dbo].[MachineLookupMethod] (
    [Id]            BIGINT IDENTITY (1, 1) NOT NULL,
    [MachineId]     INT    NULL,
    [MethodId]      BIGINT NULL,
    [DefaultMethod] BIT    NULL,
    CONSTRAINT [PK_tbl_machine_lookup_methods] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_machines_tbl_machine_lookup_methods] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([MachineId])
);

