CREATE TABLE [dbo].[CampaignEmailVariable] (
    [VariableId]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [VariableName]      VARCHAR (200) NULL,
    [RefTableName]      VARCHAR (200) NULL,
    [RefFieldName]      VARCHAR (50)  NULL,
    [CriteriaFieldName] VARCHAR (50)  NULL,
    [Description]       VARCHAR (200) NULL,
    [SectionId]         INT           NULL,
    [VariableTag]       VARCHAR (50)  NULL,
    [Key]               VARCHAR (20)  NULL,
    [OrganisationId]    BIGINT        NULL,
    CONSTRAINT [PK_tbl_system_email_variables] PRIMARY KEY CLUSTERED ([VariableId] ASC),
    CONSTRAINT [FK_CampaignEmailVariable_Section] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId])
);

