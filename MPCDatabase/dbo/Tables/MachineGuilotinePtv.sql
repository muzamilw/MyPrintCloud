CREATE TABLE [dbo].[MachineGuilotinePtv] (
    [Id]                     INT IDENTITY (1, 1) NOT NULL,
    [NoofSections]           INT CONSTRAINT [DF__tbl_machi__NoofS__70099B30] DEFAULT (0) NOT NULL,
    [NoofUps]                INT CONSTRAINT [DF__tbl_machi__NoofU__70FDBF69] DEFAULT (0) NOT NULL,
    [Noofcutswithoutgutters] INT CONSTRAINT [DF__tbl_machi__Noofc__71F1E3A2] DEFAULT (0) NOT NULL,
    [Noofcutswithgutters]    INT CONSTRAINT [DF__tbl_machi__Noofc__72E607DB] DEFAULT (0) NOT NULL,
    [GuilotineId]            INT CONSTRAINT [DF__tbl_machi__Guilo__73DA2C14] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_machine_guilotine_ptv] PRIMARY KEY CLUSTERED ([Id] ASC)
);

