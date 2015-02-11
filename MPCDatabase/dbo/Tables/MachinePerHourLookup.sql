CREATE TABLE [dbo].[MachinePerHourLookup] (
    [Id]         BIGINT     IDENTITY (1, 1) NOT NULL,
    [MethodId]   BIGINT     NULL,
    [SpeedCost]  FLOAT (53) NULL,
    [Speed]      FLOAT (53) NULL,
    [SpeedPrice] FLOAT (53) CONSTRAINT [DF__tbl_machi__Speed__7C6F7215] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_machine_perhourlookup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_lookup_methods_tbl_machine_perhourlookup] FOREIGN KEY ([MethodId]) REFERENCES [dbo].[LookupMethod] ([MethodId])
);

