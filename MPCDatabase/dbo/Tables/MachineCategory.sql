CREATE TABLE [dbo].[MachineCategory] (
    [MachineCatId]    BIGINT       NOT NULL,
    [MachineCategory] VARCHAR (50) NULL,
    [Fixed]           SMALLINT     NULL,
    CONSTRAINT [PK_tbl_machine_categories] PRIMARY KEY CLUSTERED ([MachineCatId] ASC)
);

