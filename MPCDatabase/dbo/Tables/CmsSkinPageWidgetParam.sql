CREATE TABLE [dbo].[CmsSkinPageWidgetParam] (
    [PageWidgetParamId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [PageWidgetId]      BIGINT         NULL,
    [ParamName]         NVARCHAR (100) NULL,
    [ParamValue]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_tbl_cmsSkinPageWidgetParams] PRIMARY KEY CLUSTERED ([PageWidgetParamId] ASC),
    CONSTRAINT [FK_tbl_cmsSkinPageWidgetParams_tbl_cmsSkinPageWidgets] FOREIGN KEY ([PageWidgetId]) REFERENCES [dbo].[CmsSkinPageWidget] ([PageWidgetId])
);

