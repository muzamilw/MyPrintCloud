CREATE TABLE [dbo].[CmsSkinPageWidget] (
    [PageWidgetId]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [PageId]         BIGINT   NULL,
    [WidgetId]       BIGINT   NULL,
    [SkinId]         BIGINT   NULL,
    [Sequence]       SMALLINT NULL,
    [CompanyId]      BIGINT   NULL,
    [OrganisationId] BIGINT   NULL,
    CONSTRAINT [PK_tbl_cmsPageWidgets] PRIMARY KEY CLUSTERED ([PageWidgetId] ASC),
    FOREIGN KEY ([PageId]) REFERENCES [dbo].[CmsPage] ([PageId]),
    CONSTRAINT [FK_Company_CmsSkinPageWidget] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_tbl_cmsPageWidgets_tbl_cmsPages] FOREIGN KEY ([OrganisationId]) REFERENCES [dbo].[Organisation] ([OrganisationId]),
    CONSTRAINT [FK_tbl_cmsPageWidgets_tbl_cmsWidgets] FOREIGN KEY ([WidgetId]) REFERENCES [dbo].[Widgets] ([WidgetId]),
    CONSTRAINT [FK_tbl_cmsSkinPageWidgets_tbl_cmsSkins] FOREIGN KEY ([SkinId]) REFERENCES [dbo].[tbl_cmsSkins] ([SkinId])
);

