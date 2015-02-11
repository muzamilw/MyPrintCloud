CREATE TABLE [dbo].[CostCentreTemplate] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (30) NOT NULL,
    [Header] TEXT         NOT NULL,
    [Footer] TEXT         NOT NULL,
    [Middle] TEXT         NOT NULL,
    [Type]   INT          CONSTRAINT [DF__tbl_costce__Type__0D7A0286] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_costcentretemplates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

