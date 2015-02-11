CREATE TABLE [dbo].[PhraseField] (
    [FieldId]        INT          IDENTITY (1, 1) NOT NULL,
    [FieldName]      VARCHAR (50) NULL,
    [SectionId]      INT          NULL,
    [SortOrder]      INT          NULL,
    [OrganisationId] BIGINT       NULL,
    CONSTRAINT [PK_tbl_phrase_fields] PRIMARY KEY CLUSTERED ([FieldId] ASC),
    CONSTRAINT [fk_sectionId] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId])
);

