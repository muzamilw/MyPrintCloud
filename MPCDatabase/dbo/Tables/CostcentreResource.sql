CREATE TABLE [dbo].[CostcentreResource] (
    [CostCentreId]         BIGINT           NULL,
    [ResourceId]           UNIQUEIDENTIFIER NULL,
    [CostCenterResourceId] INT              IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_tbl_costcentre_resources] PRIMARY KEY CLUSTERED ([CostCenterResourceId] ASC),
    CONSTRAINT [FK_tbl_costcentre_resources_tbl_costcentres] FOREIGN KEY ([CostCentreId]) REFERENCES [dbo].[CostCentre] ([CostCentreId])
);

