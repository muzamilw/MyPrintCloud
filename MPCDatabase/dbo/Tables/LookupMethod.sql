CREATE TABLE [dbo].[LookupMethod] (
    [MethodId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (250) NOT NULL,
    [Type]           BIGINT        NULL,
    [LockedBy]       INT           NULL,
    [OrganisationID] INT           CONSTRAINT [DF__tbl_looku__Compa__4EA8A765] DEFAULT (0) NOT NULL,
    [FlagId]         INT           CONSTRAINT [DF__tbl_looku__FlagI__4F9CCB9E] DEFAULT (0) NULL,
    [SystemSiteId]   INT           CONSTRAINT [DF__tbl_looku__Syste__5090EFD7] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_lookup_methods] PRIMARY KEY CLUSTERED ([MethodId] ASC)
);

