CREATE TABLE [dbo].[Role] (
    [RoleId]          INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]        VARCHAR (100) NOT NULL,
    [RoleDescription] TEXT          NULL,
    [IsSystemRole]    SMALLINT      CONSTRAINT [DF__tbl_roles__IsSys__2724C5F0] DEFAULT (0) NOT NULL,
    [LockedBy]        INT           NULL,
    [IsCompanyLevel]  SMALLINT      CONSTRAINT [DF__tbl_roles__IsCom__2818EA29] DEFAULT (0) NOT NULL,
    [CompanyId]       INT           CONSTRAINT [DF__tbl_roles__Compa__290D0E62] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

