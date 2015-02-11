CREATE TABLE [dbo].[Phrase] (
    [PhraseId]       INT    IDENTITY (1, 1) NOT NULL,
    [Phrase]         TEXT   NULL,
    [FieldId]        INT    NULL,
    [CompanyId]      BIGINT CONSTRAINT [DF__tbl_phras__Compa__3D491139] DEFAULT ((0)) NULL,
    [OrganisationId] BIGINT CONSTRAINT [DF__tbl_phras__Syste__3E3D3572] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tbl_phrase] PRIMARY KEY CLUSTERED ([PhraseId] ASC),
    CONSTRAINT [FK_Phrase_PhraseField] FOREIGN KEY ([FieldId]) REFERENCES [dbo].[PhraseField] ([FieldId]) ON DELETE CASCADE
);

