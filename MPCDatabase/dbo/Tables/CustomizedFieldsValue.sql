CREATE TABLE [dbo].[CustomizedFieldsValue] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [FieldId] INT          CONSTRAINT [DF__tbl_custo__Field__57DD0BE4] DEFAULT (0) NULL,
    [Value]   VARCHAR (50) NULL,
    CONSTRAINT [PK_tbl_customizedfieldsvalues] PRIMARY KEY CLUSTERED ([Id] ASC)
);

