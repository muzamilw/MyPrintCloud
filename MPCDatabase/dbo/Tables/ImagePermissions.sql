CREATE TABLE [dbo].[ImagePermissions] (
    [Id]          BIGINT IDENTITY (1, 1) NOT NULL,
    [TerritoryID] BIGINT NULL,
    [ImageId]     BIGINT NULL,
    CONSTRAINT [PK_ImagePermissions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__ImagePerm__Image__60D4A69D] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[TemplateBackgroundImage] ([Id])
);

