CREATE TABLE [dbo].[SalesType] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [ValueMember]   VARCHAR (200) NULL,
    [DisplayMember] VARCHAR (200) NULL,
    CONSTRAINT [PK_tbl_salestypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

