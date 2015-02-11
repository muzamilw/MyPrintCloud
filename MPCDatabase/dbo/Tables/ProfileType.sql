CREATE TABLE [dbo].[ProfileType] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [ParentId] INT          CONSTRAINT [DF__tbl_profi__Paren__6462DE5A] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_profile_type] PRIMARY KEY CLUSTERED ([Id] ASC)
);

