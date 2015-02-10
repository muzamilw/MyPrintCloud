CREATE TABLE [dbo].[PipeLineProduct] (
    [ProductId]   INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (200) NULL,
    CONSTRAINT [PK_tbl_pipeline_products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

