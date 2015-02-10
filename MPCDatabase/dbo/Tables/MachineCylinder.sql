CREATE TABLE [dbo].[MachineCylinder] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50) NULL,
    [Circumference] FLOAT (53)   NULL,
    [Width]         FLOAT (53)   NULL,
    [SystemSiteId]  INT          CONSTRAINT [DF__tbl_machi__Syste__6B44E613] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_machine_cylinders] PRIMARY KEY CLUSTERED ([Id] ASC)
);

