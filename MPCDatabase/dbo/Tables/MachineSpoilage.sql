CREATE TABLE [dbo].[MachineSpoilage] (
    [MachineSpoilageId] INT  IDENTITY (1, 1) NOT NULL,
    [MachineId]         INT  CONSTRAINT [DF__tbl_machi__Machi__041093DD] DEFAULT (0) NULL,
    [SetupSpoilage]     INT  CONSTRAINT [DF__tbl_machi__Setup__0504B816] DEFAULT (0) NULL,
    [RunningSpoilage]   REAL NULL,
    [NoOfColors]        INT  NULL,
    CONSTRAINT [PK_tbl_machine_spoilage] PRIMARY KEY CLUSTERED ([MachineSpoilageId] ASC)
);

