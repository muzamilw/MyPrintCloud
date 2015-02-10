CREATE TABLE [dbo].[PaginationCombination] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Pagination]   INT           CONSTRAINT [DF__tbl_pagin__Pagin__21A0F6C4] DEFAULT (0) NULL,
    [Combination]  INT           CONSTRAINT [DF__tbl_pagin__Combi__22951AFD] DEFAULT (0) NULL,
    [Sequence]     INT           CONSTRAINT [DF__tbl_pagin__Seque__23893F36] DEFAULT (0) NULL,
    [Multiplier]   INT           CONSTRAINT [DF__tbl_pagin__Multi__247D636F] DEFAULT (0) NULL,
    [Sections]     INT           CONSTRAINT [DF__tbl_pagin__Secti__257187A8] DEFAULT (0) NULL,
    [Description]  VARCHAR (200) NULL,
    [SystemSiteId] INT           CONSTRAINT [DF__tbl_pagin__Syste__2665ABE1] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_pagination_combinations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

