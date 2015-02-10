CREATE TABLE [dbo].[RoleSite] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [RoleId] INT CONSTRAINT [DF__tbl_role___RoleI__1F83A428] DEFAULT (0) NOT NULL,
    [SiteId] INT CONSTRAINT [DF__tbl_role___SiteI__2077C861] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_role_sites] PRIMARY KEY CLUSTERED ([Id] ASC)
);

