CREATE TABLE [dbo].[CostCentreVariableType] (
    [CategoryId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_costcentrevariabletypes] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

