CREATE TABLE [dbo].[FieldVariable] (
    [VariableId]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [VariableName]      VARCHAR (200) NULL,
    [RefTableName]      VARCHAR (200) NULL,
    [CriteriaFieldName] VARCHAR (200) NULL,
    [VariableSectionId] INT           NULL,
    [VariableTag]       VARCHAR (200) NULL,
    [SortOrder]         INT           NULL,
    [KeyField]          VARCHAR (100) NULL,
    [VariableType]      INT           NULL,
    CONSTRAINT [PK_FieldVariables] PRIMARY KEY CLUSTERED ([VariableId] ASC)
);

