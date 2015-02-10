CREATE TABLE [dbo].[MachineClickChargeLookup] (
    [Id]          BIGINT     IDENTITY (1, 1) NOT NULL,
    [MethodId]    BIGINT     CONSTRAINT [DF__tbl_machi__Metho__5555A4F4] DEFAULT (270592626208) NOT NULL,
    [SheetCost]   FLOAT (53) NULL,
    [Sheets]      INT        NULL,
    [SheetPrice]  FLOAT (53) NULL,
    [TimePerHour] FLOAT (53) NULL,
    CONSTRAINT [PK_tbl_machine_clickchargelookup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tbl_lookup_methods_tbl_machine_clickchargelookup] FOREIGN KEY ([MethodId]) REFERENCES [dbo].[LookupMethod] ([MethodId])
);

