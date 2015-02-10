CREATE TABLE [dbo].[PaginationFinishStyle] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NULL,
    [Head]         FLOAT (53)   NULL,
    [Trim]         FLOAT (53)   NULL,
    [Foredge]      FLOAT (53)   NULL,
    [Spine]        FLOAT (53)   NULL,
    [SystemSiteId] INT          CONSTRAINT [DF__tbl_pagin__Syste__2A363CC5] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tbl_pagination_finishstyle] PRIMARY KEY CLUSTERED ([Id] ASC)
);

