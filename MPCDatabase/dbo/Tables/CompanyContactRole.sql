CREATE TABLE [dbo].[CompanyContactRole] (
    [ContactRoleId]   INT          IDENTITY (1, 1) NOT NULL,
    [ContactRoleName] VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_customeruserroles] PRIMARY KEY CLUSTERED ([ContactRoleId] ASC)
);

