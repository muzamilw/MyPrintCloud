CREATE TABLE [dbo].[ProfileCostCentreGroup] (
    [Id]                BIGINT IDENTITY (1, 1) NOT NULL,
    [ProfileId]         BIGINT CONSTRAINT [DF__tbl_profi__Profi__5DB5E0CB] DEFAULT ((270592725200.)) NOT NULL,
    [CostCentreGroupId] INT    NULL,
    CONSTRAINT [PK_tbl_profile_costcentre_groups] PRIMARY KEY CLUSTERED ([Id] ASC)
);

