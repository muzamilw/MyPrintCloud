CREATE TABLE [dbo].[SectionCostCentreResource] (
    [SectionCostCentreResourceId] INT              IDENTITY (1, 1) NOT NULL,
    [SectionCostcentreId]         INT              CONSTRAINT [DF__tbl_secti__Secti__59B045BD] DEFAULT ((270592645888.)) NULL,
    [ResourceId]                  UNIQUEIDENTIFIER NULL,
    [ResourceTime]                INT              CONSTRAINT [DF__tbl_secti__Resou__5B988E2F] DEFAULT ((0)) NULL,
    [IsScheduleable]              SMALLINT         CONSTRAINT [DF__tbl_secti__IsSch__5C8CB268] DEFAULT ((1)) NULL,
    [IsScheduled]                 SMALLINT         CONSTRAINT [DF__tbl_secti__IsSch__5D80D6A1] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tbl_section_costcentre_resources] PRIMARY KEY CLUSTERED ([SectionCostCentreResourceId] ASC),
    CONSTRAINT [FK_tbl_section_costcentre_resources_tbl_section_costcentres] FOREIGN KEY ([SectionCostcentreId]) REFERENCES [dbo].[SectionCostcentre] ([SectionCostcentreId])
);

