CREATE TABLE [dbo].[SectionFlag] (
    [SectionFlagId]   INT           IDENTITY (1, 1) NOT NULL,
    [SectionId]       INT           NULL,
    [FlagName]        VARCHAR (50)  NULL,
    [FlagColor]       VARCHAR (50)  NULL,
    [flagDescription] VARCHAR (250) NULL,
    [OrganisationId]  BIGINT        NULL,
    [FlagColumn]      VARCHAR (50)  NULL,
    [isDefault]       BIT           NULL,
    CONSTRAINT [PK_tbl_section_flags] PRIMARY KEY CLUSTERED ([SectionFlagId] ASC),
    CONSTRAINT [FK_tbl_section_flags_tbl_sections] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId])
);

