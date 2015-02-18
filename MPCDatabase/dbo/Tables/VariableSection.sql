CREATE TABLE [dbo].[VariableSection] (
    [VariableSectionId]  BIGINT        NOT NULL,
    [SectionName]        VARCHAR (200) NULL,
    [SectionDescription] VARCHAR (500) NULL,
    CONSTRAINT [PK_VariableSection] PRIMARY KEY CLUSTERED ([VariableSectionId] ASC)
);

