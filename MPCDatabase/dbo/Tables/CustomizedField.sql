CREATE TABLE [dbo].[CustomizedField] (
    [FieldId]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [FieldName] VARCHAR (20) NULL,
    [FieldType] INT          CONSTRAINT [DF__tbl_custo__Field__4E53A1AA] DEFAULT (0) NULL,
    [CompanyId] INT          CONSTRAINT [DF__tbl_custo__Compa__4F47C5E3] DEFAULT (0) NULL,
    CONSTRAINT [PK_tbl_customizedfields] PRIMARY KEY CLUSTERED ([FieldId] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [PK]
    ON [dbo].[CustomizedField]([FieldId] ASC);

