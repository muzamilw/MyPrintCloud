CREATE TABLE [dbo].[Roleright] (
    [RRId]    INT IDENTITY (1, 1) NOT NULL,
    [RoleId]  INT CONSTRAINT [DF__tbl_roler__RoleI__2354350C] DEFAULT (0) NOT NULL,
    [RightId] INT CONSTRAINT [DF__tbl_roler__Right__24485945] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_rolerights] PRIMARY KEY CLUSTERED ([RRId] ASC),
    CONSTRAINT [FK_tbl_rolerights_tbl_rights] FOREIGN KEY ([RightId]) REFERENCES [dbo].[AccessRight] ([RightId]),
    CONSTRAINT [FK_tbl_rolerights_tbl_roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);


GO
CREATE NONCLUSTERED INDEX [RightID]
    ON [dbo].[Roleright]([RightId] ASC);


GO
CREATE NONCLUSTERED INDEX [RoleID]
    ON [dbo].[Roleright]([RoleId] ASC);

