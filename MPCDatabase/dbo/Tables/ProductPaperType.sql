CREATE TABLE [dbo].[ProductPaperType] (
    [PaperTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [PaperType]   VARCHAR (100) NULL,
    CONSTRAINT [PK_tbl_ProductPaperType] PRIMARY KEY CLUSTERED ([PaperTypeId] ASC)
);

