CREATE TABLE [dbo].[ScheduledCostCenter] (
    [ScheduledCostCenterId] INT           IDENTITY (1, 1) NOT NULL,
    [SectionCostCenterId]   INT           CONSTRAINT [DF__tbl_sched__Secti__3943762B] DEFAULT (0) NOT NULL,
    [CostCenterId]          INT           CONSTRAINT [DF__tbl_sched__CostC__3A379A64] DEFAULT (0) NOT NULL,
    [SystemCostCenterType]  INT           CONSTRAINT [DF__tbl_sched__Syste__3B2BBE9D] DEFAULT (0) NOT NULL,
    [ResourcesId]           INT           CONSTRAINT [DF__tbl_sched__Resou__3C1FE2D6] DEFAULT (0) NOT NULL,
    [CritMints]             FLOAT (53)    CONSTRAINT [DF__tbl_sched__CritM__3D14070F] DEFAULT (0) NOT NULL,
    [SetupTime]             FLOAT (53)    CONSTRAINT [DF__tbl_sched__Setup__3E082B48] DEFAULT (0) NOT NULL,
    [RunTime]               FLOAT (53)    CONSTRAINT [DF__tbl_sched__RunTi__3EFC4F81] DEFAULT (0) NOT NULL,
    [WorkInstructions]      TEXT          NOT NULL,
    [CostCenterName]        VARCHAR (255) NOT NULL,
    [Notes]                 CHAR (10)     NULL,
    [EstimatedStartTime]    DATETIME      NULL,
    [EstimatedEndTime]      DATETIME      NULL,
    CONSTRAINT [PK_tbl_scheduled_costcenters] PRIMARY KEY CLUSTERED ([ScheduledCostCenterId] ASC)
);

