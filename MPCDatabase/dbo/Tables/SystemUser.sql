CREATE TABLE [dbo].[SystemUser] (
    [SystemUserId]      UNIQUEIDENTIFIER NOT NULL,
    [UserName]          VARCHAR (100)    NOT NULL,
    [Description]       VARCHAR (100)    NULL,
    [OrganizationId]    INT              NULL,
    [DepartmentId]      INT              NULL,
    [FullName]          VARCHAR (100)    NULL,
    [UserType]          VARCHAR (100)    NULL,
    [RoleId]            INT              CONSTRAINT [DF__tbl_syste__RoleI__027D5126] DEFAULT ((0)) NULL,
    [IsAccountDisabled] SMALLINT         CONSTRAINT [DF__tbl_syste__IsAcc__0371755F] DEFAULT ((0)) NULL,
    [CostPerHour]       REAL             NULL,
    [IsScheduleable]    SMALLINT         CONSTRAINT [DF__tbl_syste__IsSch__04659998] DEFAULT ((0)) NULL,
    [IsSystemUser]      INT              CONSTRAINT [DF_tbl_systemusers_IsSystemUser] DEFAULT ((0)) NULL,
    [UserAuthToken]     NVARCHAR (500)   NULL,
    CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED ([SystemUserId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [RoleID]
    ON [dbo].[SystemUser]([RoleId] ASC);

