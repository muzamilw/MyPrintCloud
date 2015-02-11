CREATE TABLE [dbo].[Widgets] (
    [WidgetId]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [WidgetCode]        VARCHAR (10)   NOT NULL,
    [WidgetName]        VARCHAR (100)  NULL,
    [WidgetControlName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_cmsWidgets] PRIMARY KEY CLUSTERED ([WidgetId] ASC)
);

