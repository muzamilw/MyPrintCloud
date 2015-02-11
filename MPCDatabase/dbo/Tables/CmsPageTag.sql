CREATE TABLE [dbo].[CmsPageTag] (
    [TagId]     BIGINT NULL,
    [PageId]    BIGINT NULL,
    [PageTagId] INT    IDENTITY (1, 1) NOT NULL,
    [CompanyId] BIGINT NULL,
    CONSTRAINT [PK_tbl_cmsPageTags] PRIMARY KEY CLUSTERED ([PageTagId] ASC),
    CONSTRAINT [FK_webcmsPageTags_webcmsPages] FOREIGN KEY ([PageId]) REFERENCES [dbo].[CmsPage] ([PageId]),
    CONSTRAINT [FK_webcmsPageTags_webcmsTags] FOREIGN KEY ([TagId]) REFERENCES [dbo].[CmsTags] ([TagId])
);

