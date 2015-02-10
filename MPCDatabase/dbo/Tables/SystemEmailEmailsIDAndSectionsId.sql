CREATE TABLE [dbo].[SystemEmailEmailsIDAndSectionsId] (
    [Id]        INT       IDENTITY (1, 1) NOT NULL,
    [EmailId]   INT       NULL,
    [SectionId] CHAR (10) NULL,
    CONSTRAINT [PK_tbl_system_email_emailsID_and_sectionsID] PRIMARY KEY CLUSTERED ([Id] ASC)
);

