CREATE TABLE [dbo].[RoleSection] (
    [RSId]      INT IDENTITY (1, 1) NOT NULL,
    [RoleId]    INT CONSTRAINT [DF__tbl_roles__RoleI__2BE97B0D] DEFAULT (0) NOT NULL,
    [SectionId] INT CONSTRAINT [DF__tbl_roles__Secti__2CDD9F46] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_rolesections] PRIMARY KEY CLUSTERED ([RSId] ASC),
    CONSTRAINT [FK_tbl_rolesections_tbl_roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]),
    CONSTRAINT [FK_tbl_rolesections_tbl_sections] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId])
);


GO
CREATE NONCLUSTERED INDEX [RoleID]
    ON [dbo].[RoleSection]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [SectionID]
    ON [dbo].[RoleSection]([SectionId] ASC);

