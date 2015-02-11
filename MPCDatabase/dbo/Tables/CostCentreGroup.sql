CREATE TABLE [dbo].[CostCentreGroup] (
    [GroupId]      INT           IDENTITY (1, 1) NOT NULL,
    [GroupName]    VARCHAR (100) NOT NULL,
    [Sequence]     SMALLINT      CONSTRAINT [DF__tbl_costc__Seque__47DBAE45] DEFAULT (0) NOT NULL,
    [SystemSiteId] INT           CONSTRAINT [DF__tbl_costc__Syste__48CFD27E] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_costcentre_groups] PRIMARY KEY CLUSTERED ([GroupId] ASC)
);

