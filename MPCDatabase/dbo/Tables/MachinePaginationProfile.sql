CREATE TABLE [dbo].[MachinePaginationProfile] (
    [Id]           BIGINT IDENTITY (1, 1) NOT NULL,
    [MachineId]    INT    NULL,
    [PaginationId] INT    NULL,
    CONSTRAINT [PK_tbl_machine_pagination_profile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_machines_tbl_machine_pagination_profile] FOREIGN KEY ([MachineId]) REFERENCES [dbo].[Machine] ([MachineId])
);

