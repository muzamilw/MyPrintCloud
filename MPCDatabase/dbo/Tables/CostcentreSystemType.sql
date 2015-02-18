CREATE TABLE [dbo].[CostcentreSystemType] (
    [SystemTypeId]   INT          IDENTITY (1, 1) NOT NULL,
    [SystemTypeName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tbl_costcentre_systemtypes] PRIMARY KEY CLUSTERED ([SystemTypeId] ASC)
);

