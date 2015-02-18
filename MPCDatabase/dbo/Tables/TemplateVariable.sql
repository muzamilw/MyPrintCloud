CREATE TABLE [dbo].[TemplateVariable] (
    [ProductVariableId] BIGINT IDENTITY (1, 1) NOT NULL,
    [TemplateId]        INT    NULL,
    [VariableId]        INT    NULL,
    CONSTRAINT [PK_ProductVariable] PRIMARY KEY CLUSTERED ([ProductVariableId] ASC)
);

