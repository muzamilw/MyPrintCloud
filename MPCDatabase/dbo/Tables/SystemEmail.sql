CREATE TABLE [dbo].[SystemEmail] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Title]        VARCHAR (200) NULL,
    [FFrom]        VARCHAR (200) NULL,
    [FromEmail]    VARCHAR (200) NULL,
    [Subject]      TEXT          NULL,
    [Body]         TEXT          NULL,
    [TextBody]     TEXT          NULL,
    [LockedBy]     INT           CONSTRAINT [DF__tbl_syste__Locke__77FFC2B3] DEFAULT (0) NULL,
    [SystemSiteId] INT           CONSTRAINT [DF__tbl_syste__Syste__78F3E6EC] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_system_emails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

