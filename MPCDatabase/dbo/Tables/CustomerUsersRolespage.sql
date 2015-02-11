CREATE TABLE [dbo].[CustomerUsersRolespage] (
    [Id]           INT  IDENTITY (1, 1) NOT NULL,
    [RoleId]       INT  NOT NULL,
    [allowPages]   TEXT NULL,
    [PageURL]      TEXT NULL,
    [IsApprover]   BIT  NULL,
    [DisplayOrder] INT  CONSTRAINT [DF_tbl_customer_users_rolespages_DisplayOrder] DEFAULT (0) NULL
);

